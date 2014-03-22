using UnityEngine;
using UnityEditor;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Mono.Data.SqliteClient;


public class SQLiteManager:EditorWindow
{


		static SQLiteManager
				_instance;
		static SQLiteManager
				Instance {
				get {
						if (_instance == null)
								Init ();
						return _instance;
				}
		}
		[MenuItem ("Window/SQLiteManager")]
		static void Init ()
		{
				_instance = (SQLiteManager)EditorWindow.GetWindow (typeof(SQLiteManager));
		}



		List<System.Action>	repaintActions = new List<System.Action> ();

		[System.NonSerialized]
		SQLiteManagerModel
				model = new SQLiteManagerModel ();

		UnityEngine.Object dbSlot;
		bool hasDB = false; 
		[System.NonSerialized]
		public int
				page = 0;
		public int rowsPerPage = 15;



		[System.NonSerialized]
		public bool
				dirty = true; //Should we reload all database info?
		[System.NonSerialized]
		public bool
				isReady = false;
		public int rowCount;
		public string[] columnNames;
		public string[] types;
		public string[] tableInfo;
		public object[] insertRow;
		public object[] currentRows;
		public ColumnObject[] tableLayout;
		public Dictionary<string,System.Type> dataTypes;
		public string[] availableTypes;
	

		Vector2 sideBarScrollPosition = Vector2.zero;
		Vector2 mainScreenScrollPosition = Vector2.zero;
		string screen = "CurrentTable";
	
		string tableViewTab = "Content";
		string workingTable;


		Dictionary<string,Dictionary<string,ISQLiteManagerView>> views;



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

		void OnEnable ()
		{
				Debug.Log ("enable");
		}

		void OnDisable ()
		{
				Debug.Log ("disable");
		}

		void Update ()
		{
				if (dataTypes == null) {
						dataTypes = SQLiteDataTypeLoader.GetDataTypes ();
						availableTypes = new string[dataTypes.Keys.Count];
						dataTypes.Keys.CopyTo (availableTypes, 0);
				}

				if (views == null) {
		
						CollectViews ();
				}


				model.SetRange (page, rowsPerPage); 


				if (workingTable == null) {
						workingTable = tableInfo [0];
				}
				model.SetCurrentTable (workingTable);

		

				if (dirty || model.hasChanged) {
						if (hasDB = model.HasDB ()) {
								DoRepaint (); //Carry out any action that might have been added from OnGUI()
								tableInfo = model.GetTableInfo ();
				

				
			
								if (isReady = model.isReady) {
										rowCount = model.rowCount;
										columnNames = model.GetColumnNames ();
										types = model.GetTypes ();
										insertRow = model.GetInsertRow ();
										currentRows = model.GetCurrentRows ();
										tableLayout = model.GetTableLayout ().ToArray ();

								}


						} else {
						}
						dirty = false;
				}

		}

		public static void SetDirty ()
		{
				SQLiteManager.Instance.dirty = true;
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


		void CollectViews ()
		{
				views = new Dictionary<string, Dictionary<string, ISQLiteManagerView>> ();
		

				IEnumerable<System.Type> types = GetSubTypes (typeof(ISQLiteManagerView));
		
				foreach (System.Type t in types) {
						ISQLiteManagerView instance = (ISQLiteManagerView)Activator.CreateInstance (t);
						if (views.ContainsKey (instance.type)) {
								views [instance.type].Add (instance.name, instance);
						} else {
								views.Add (instance.type, new Dictionary<string, ISQLiteManagerView> (){{instance.name,instance}});
				
						}
			
			
				}
		}

		void CollectTypes ()
		{

		}

		IEnumerable<System.Type> GetSubTypes (System.Type type)
		{
				var assembly = type.Assembly;
				return assembly.GetTypes ().Where (t => t.IsSubclassOf (type));
		}

	
		void OnGUI ()
		{

				if (!hasDB) {

						DBSelector ();

				} else {
						float width = position.width;
						float height = position.height;

						float sideBarWidth = width / 100 * 20;
						Rect sideBarRect = new Rect (0, 0, sideBarWidth, height);
						GUILayout.BeginArea (sideBarRect);
						sideBarScrollPosition = GUILayout.BeginScrollView (sideBarScrollPosition);
						Sidebar ();
						GUILayout.EndScrollView ();
						GUILayout.EndArea ();

						GUILayout.BeginArea (new Rect (sideBarWidth + 15, 0, width / 100 * 80 - 15, height));
						mainScreenScrollPosition = GUILayout.BeginScrollView (mainScreenScrollPosition);

						ShowView ("screen", screen);
						GUILayout.EndScrollView ();

						GUILayout.EndArea ();

				}
		
		}

		void DBSelector ()
		{
				dbSlot = EditorGUILayout.ObjectField (dbSlot, typeof(UnityEngine.Object), true);
				if (dbSlot != null) {
						string workingDB = Application.dataPath + "/../" + AssetDatabase.GetAssetPath (dbSlot);
						model.SetDB (workingDB);
						SQLiteManager.SetDirty ();
				}

				//TODO "Or create a new Database"
		}



		void Sidebar ()
		{

				foreach (string table in tableInfo) {

						GUI.enabled = (table != workingTable);
			
						if (GUILayout.Button (table)) {
								workingTable = table;
								screen = "CurrentTable";
								SQLiteManager.SetDirty ();
				
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
						SQLiteManager.SetDirty ();
			
				}
		}
	
}
