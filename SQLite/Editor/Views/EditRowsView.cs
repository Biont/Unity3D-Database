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

						GUILayout.Label ("Displaying " + data.currentRows.Length + " of " + data.rowCount + " rows", EditorStyles.label);
			
						int currentRows = data.currentRows.Length;
						if (selectedRows == null || selectedRows.Length != currentRows) {
								selectedRows = new bool[currentRows];
						}


//						GUILayout.Label ("Selected table " + model.workingTable, EditorStyles.boldLabel);
			
						//Top row
						EditorGUILayout.BeginHorizontal ();
						foreach (string name in data.columnNames) {
								GUILayout.Label (name, EditorStyles.label);
						}
						EditorGUILayout.EndHorizontal ();
						
						if (currentRows > 0) {
								DisplayCurrentRows ();
						}
						DisplayPagination ();
			
						DisplayInsertRow ();
			
				} else {
						GUILayout.Label ("Select a table from the left", EditorStyles.boldLabel);
			
				}
		
		
		}


		void DisplayCurrentRows ()
		{ 

//				Debug.Log ("currows selrows " + data.currentRows.Length + " - " + selectedRows.Length);
		

				int rowIndex = 0;
				string[] types = data.types;
				if (data.isReady) {

						foreach (ColumnObject[] row in data.currentRows) {
								EditorGUILayout.BeginHorizontal ();
								selectedRows [rowIndex] = EditorGUILayout.Toggle (selectedRows [rowIndex], GUILayout.MaxWidth (20));
			
			
								int colIndex = 0;
								foreach (var column in row) {
										column.EditorField (selectedRows [rowIndex]);
								}
			
								if (GUILayout.Button ("Save")) {
										//UpdateRow(rowIndex);
								}
			
								rowIndex++;
								EditorGUILayout.EndHorizontal ();
			
						}
						EditorGUILayout.BeginHorizontal ();


						if (GUILayout.Button ("Select all")) {
								int rowLen = selectedRows.Length;
								for (int i=0; i<rowLen; i++) {
										selectedRows [i] = true;
								}
						}
						if (GUILayout.Button ("Select none")) {
								int rowLen = selectedRows.Length;
								for (int i=0; i<rowLen; i++) {
										selectedRows [i] = false;
								}
						}

						GUILayout.Label ("Selected rows:", EditorStyles.label);
		
						if (GUILayout.Button ("Delete", GUILayout.Width (80))) {
								if (EditorUtility.DisplayDialog ("Confirm deletion?", "Really delete this row?", "yep", "nope")) {
										//TODO
								}
						}
						if (GUILayout.Button ("Export", GUILayout.Width (80))) {
								if (EditorUtility.DisplayDialog ("Confirm deletion?", "Really delete this row?", "yep", "nope")) {
										//TODO
								}
						}
		
						EditorGUILayout.EndHorizontal ();


				}
				
		}

		void DisplayPagination ()
		{
				int maxPages = Mathf.RoundToInt (data.rowCount / data.rowsPerPage);

				EditorGUILayout.BeginHorizontal ();
				GUI.enabled = (data.page > 0);
			
				if (GUILayout.Button ("<<", GUILayout.Width (50))) {
						data.page--;
				}
				GUI.enabled = true;

				GUILayout.Label ("Page: " + data.page, EditorStyles.label);
		

				GUI.enabled = (data.page < maxPages);
		
				if (GUILayout.Button (">>", GUILayout.Width (50))) {
						data.page++;
				}
				GUI.enabled = true;
		
				EditorGUILayout.EndHorizontal ();
		}





		void DisplayInsertRow ()
		{
				EditorGUILayout.BeginHorizontal ();
				int index = 0;
				foreach (ColumnObject col in data.insertRow) {
						col.EditorField (false);
				}
		
				if (GUILayout.Button ("Insert")) {
						data.InsertRow ();
				}
		
				EditorGUILayout.EndHorizontal ();
				data.UpdateInsertRow ();
		}
}
