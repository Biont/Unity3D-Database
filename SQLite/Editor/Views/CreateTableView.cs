using UnityEngine;
using UnityEditor;
using System.Collections;
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

		private int[] typeIndices;
		private List<ColumnObject> tempTable = new List<ColumnObject> ();
	
	
		// Update is called once per frame
		public override void Display ()
		{
				GUILayout.Label ("createtableview", EditorStyles.label);

				GetTypeIndices ();

				foreach (ColumnObject col in tempTable) {
						col.EditorFieldSetup ();
				}


				if (GUILayout.Button ("Add Column")) {
						AddColumn ();
				}
	
		}

		void AddColumn ()
		{

				tempTable.Add (new ColumnObject ("", data.availableTypes [0], false, null, false));

		}
	
		private void GetTypeIndices ()
		{
//				int numCols = data.tableLayout.Length;
//				if (typeIndices == null || typeIndices.Length != numCols) {
//						typeIndices = new int[data.tableLayout.Length];
//						for (int i=0; i<numCols; i++) {
//								typeIndices [i] = Array.IndexOf (data.availableTypes, data.tableLayout [i].type);
//						}
//				}
		}
}
