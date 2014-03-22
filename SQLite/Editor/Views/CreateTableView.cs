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



				tempTable.ListTool ((col) => {

						col.EditorFieldSetup (data.availableTypes);

				}, () => {

						return new ColumnObject ("Column name", data.availableTypes [0], false, null, false);

				});

	
		}

		void AddColumn ()
		{

				tempTable.Add (new ColumnObject ("", data.availableTypes [0], false, null, false));

		}
	

}
