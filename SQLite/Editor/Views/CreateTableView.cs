using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class CreateTableView : ISQLiteManagerView
{

		public override string type {
				get {
						return "screen";
				}
		}
	
		public override string name {
				get {
						return "CreateTable";
				}
		}

		private string tableName = "Table";
		private int[] typeIndices;
		private List<ColumnObject> tempTable = new List<ColumnObject> ();
	
	
		// Update is called once per frame
		public override void Display ()
		{
				GUILayout.Label ("createtableview", EditorStyles.label);

				if (GUILayout.Button ("?")) {
						EditorUtility.DisplayDialog (
				"Create Table",
				"This is the help dialog for table creation",
				"Ok");
				}
				tableName = EditorGUILayout.TextField ("Table Name", tableName);
				tempTable.ListTool ((col) => {

						col.EditorFieldSetup (data.availableTypes);

				}, () => {

						return new ColumnObject ("Column name", data.availableTypes [0], false, null, false);

				});

				if (GUILayout.Button ("Write")) {
						string sql = string.Format ("CREATE TABLE {0}({1})", tableName, tempTable.Implode (","));
						tempTable.Clear ();
						data.SQL (sql);

				}
		}

		void AddColumn ()
		{

				tempTable.Add (new ColumnObject ("", data.availableTypes [0], false, null, false));

		}
	

}
