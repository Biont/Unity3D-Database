using UnityEngine;
using UnityEditor;

using System.Collections;

public class EditRowsView : ISQLiteManagerView
{

		public override string type {
				get {
						return "tableTab";
				}
		}
	
		public override string name {
				get {
						return "Content";
				}
		}


		bool[] selectedRows;
	
	
		// Update is called once per frame
		public override void Display ()
		{
				GUILayout.Label ("currenttable", EditorStyles.label);


		
		
				if (data.isReady) {


			
						if (selectedRows == null || selectedRows.Length != data.rowsPerPage) {
								selectedRows = new bool[data.rowsPerPage];
						}


//						GUILayout.Label ("Selected table " + model.workingTable, EditorStyles.boldLabel);
			
						//Top row
						EditorGUILayout.BeginHorizontal ();
						foreach (string name in data.columnNames) {
								GUILayout.Label (name, EditorStyles.label);
						}
						EditorGUILayout.EndHorizontal ();
						// Fetched rows
						DisplayCurrentRows ();
			
						DisplayInsertRow ();
			
				} else {
						GUILayout.Label ("Select a table from the left", EditorStyles.boldLabel);
			
				}
		
		
		}


		void DisplayCurrentRows ()
		{ 
				int rowIndex = 0;
				string[] types = data.types;
				foreach (object[] row in data.currentRows) {
						EditorGUILayout.BeginHorizontal ();
						selectedRows [rowIndex] = EditorGUILayout.Toggle (selectedRows [rowIndex], GUILayout.MaxWidth (20));
			
			
						int colIndex = 0;
						foreach (var column in row) {
				
								switch (types [colIndex]) {
								case "TEXT":
										row [colIndex] = EditorGUILayout.TextField ((string)row [colIndex]);
										break;
								case "INTEGER":
										row [colIndex] = EditorGUILayout.IntField ((int)row [colIndex]);
										break;
								case "REAL":
										row [colIndex] = EditorGUILayout.FloatField ((float)row [colIndex]);
										break;
								default:
										GUILayout.Label ("unknown type", EditorStyles.label);
										break;
								}
				
								colIndex++;
				
				
				
						}
			
						if (GUILayout.Button ("Save")) {
								//UpdateRow(rowIndex);
						}
			
						rowIndex++;
						EditorGUILayout.EndHorizontal ();
			
				}
				EditorGUILayout.BeginHorizontal ();
		
				GUILayout.Label ("Selected rows:", EditorStyles.label);
		
				if (GUILayout.Button ("Delete")) {
						if (EditorUtility.DisplayDialog ("Confirm deletion?", "Really delete this row?", "yep", "nope")) {
								//TODO
						}
				}
				if (GUILayout.Button ("Export")) {
						if (EditorUtility.DisplayDialog ("Confirm deletion?", "Really delete this row?", "yep", "nope")) {
								//TODO
						}
				}
		
				EditorGUILayout.EndHorizontal ();

		}

		void DisplayInsertRow ()
		{
				EditorGUILayout.BeginHorizontal ();
				int index = 0;
				foreach (string type in data.types) {
						switch (type) {
						case "TEXT":
								if (data.insertRow [index] == null) {
										data.insertRow [index] = "";
								}
								data.insertRow [index] = EditorGUILayout.TextField ((string)data.insertRow [index]);
								break;
						case "INTEGER":
								if (data.insertRow [index] == null) {
										data.insertRow [index] = 0;
								}
								data.insertRow [index] = EditorGUILayout.IntField ((int)data.insertRow [index]);
								break;
						case "REAL":
								if (data.insertRow [index] == null) {
										data.insertRow [index] = 0.0f;
								}
								data.insertRow [index] = EditorGUILayout.FloatField ((float)data.insertRow [index]);
								break;
						default:
								GUILayout.Label ("unknown type", EditorStyles.label);
								break;
						}
						index++;
				}
		
				if (GUILayout.Button ("Insert")) {
						data.InsertRow ();
				}
		
				EditorGUILayout.EndHorizontal ();
				data.UpdateInsertRow ();
		}
}
