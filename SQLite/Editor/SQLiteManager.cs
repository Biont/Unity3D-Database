using UnityEngine;
using UnityEditor;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Mono.Data.SqliteClient;

public class SQLiteManager : EditorWindow
{
		[MenuItem ("Window/SQLiteManager")]
		static void Init ()
		{
				SQLiteManager window = (SQLiteManager)EditorWindow.GetWindow (typeof(SQLiteManager));
		}
		[System.NonSerialized]
		List<System.Action>
				repaintActions = new List<System.Action> ();
		[System.NonSerialized]
		SQLiteManagerModel
				model = new SQLiteManagerModel ();

		UnityEngine.Object dbSlot;
		bool hasDB = false; 

		public int rowsPerPage = 15;

		public bool isReady;
		public string[] columnNames;
		public string[] types;
		string[] tableInfo;
		public object[] insertRow;
		public object[] currentRows;


		Vector2 sideBarScrollPosition = Vector2.zero;
		string screen = "CurrentTable";
	
		string tableViewTab = "Content";
		string workingTable;


		Dictionary<string,Dictionary<string,ISQLiteManagerView>> views;

	

		private int tableIndex;



		public void ShowView (string viewType, string viewName)
		{
				if (views.ContainsKey (viewType)) {
						if (views [viewType].ContainsKey (viewName)) {
								views [viewType] [viewName].Prepare (this);
						}
				}
		}
	
		public string[] GetViewNames (string viewType)
		{
				if (views.ContainsKey (viewType)) {
						return views [viewType].Keys.ToArray ();
				}
				Debug.LogError ("GetViewNames() Could not find the viewType " + viewType);
				return null;
		}

		public void InsertRow ()
		{
				OnRepaint (() => {
			
						model.Insert (insertRow);
			
			
				});
		}
	
		public void UpdateInsertRow ()
		{
				model.SetInsertRow (insertRow);
		}

		void Update ()
		{
				if (views == null) {
						views = new Dictionary<string, Dictionary<string, ISQLiteManagerView>> ();

						var baseType = typeof(ISQLiteManagerView);
						var assembly = baseType.Assembly;
						IEnumerable<System.Type> types = assembly.GetTypes ().Where (t => t.IsSubclassOf (baseType));
		
						foreach (System.Type t in types) {
								ISQLiteManagerView instance = (ISQLiteManagerView)Activator.CreateInstance (t);
								if (views.ContainsKey (instance.type)) {
										views [instance.type].Add (instance.name, instance);
								} else {
										views.Add (instance.type, new Dictionary<string, ISQLiteManagerView> (){{instance.name,instance}});
					
								}
						
		
						}
				}
		
				if (hasDB = model.HasDB ()) {
						DoRepaint (); //Carry out any action that might have been added from OnGUI()

						tableInfo = model.GetTableInfo ();
			
						if (isReady = model.IsReady ()) {
								columnNames = model.GetColumnNames ();
								types = model.GetTypes ();
								insertRow = model.GetInsertRow ();
								currentRows = model.GetCurrentRows ();


						}


				} else {
				}


		}

		void OnRepaint (System.Action action)
		{
				repaintActions.Add (action);
		}

		void DoRepaint ()
		{
				foreach (System.Action action in repaintActions) {
						action ();
				}
				repaintActions.Clear ();
		}


	
		void OnGUI ()
		{

				if (!hasDB) {

						DBSelector ();

				} else {
						Rect sideBarRect = new Rect (0, 0, 110, 200);
						GUILayout.BeginArea (sideBarRect);
						sideBarScrollPosition = GUILayout.BeginScrollView (sideBarScrollPosition);
						Sidebar ();
						GUILayout.EndScrollView ();
						GUILayout.EndArea ();

						GUILayout.BeginArea (new Rect (120, 0, 500, 500));

						ShowView ("screen", screen);

						GUILayout.EndArea ();

				}
		
		}

		void DBSelector ()
		{
				dbSlot = EditorGUILayout.ObjectField (dbSlot, typeof(UnityEngine.Object), true);
				if (dbSlot != null) {
						string workingDB = Application.dataPath + "/../" + AssetDatabase.GetAssetPath (dbSlot);
						model.SetDB (workingDB);
				}

				//TODO "Or create a new Database"
		}



		void Sidebar ()
		{
				int tableLen = tableInfo.Length;
		

				GUILayout.Label (tableLen + " Tables", EditorStyles.boldLabel);
				for (int i=0; i<tableLen; i++) {

						GUI.enabled = (tableInfo [i] != workingTable);
			
						if (GUILayout.Button (tableInfo [i])) {
								workingTable = tableInfo [i];
								screen = "CurrentTable";
								tableIndex = i;
								OnRepaint (() => {
										model.SetCurrentTable (tableInfo [tableIndex]);
								});
						}
						GUI.enabled = true;
			
			
				}
				GUILayout.Space (10.0f);

				GUI.enabled = (screen != "CreateTable");
				if (GUILayout.Button ("Create Table")) {
						workingTable = null;
						screen = "CreateTable";
			
				}
				GUI.enabled = true;
		
				GUI.enabled = (screen != "Settings");
				if (GUILayout.Button ("Settings")) {
						workingTable = null;
			
						screen = "Settings";
			
				}
				GUI.enabled = true;
		
				if (GUILayout.Button ("Change DB")) {
						dbSlot = null;
						model.ClearDB ();
				}
		}
	
}
