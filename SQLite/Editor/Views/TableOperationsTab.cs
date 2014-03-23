using UnityEngine;
using UnityEditor;

using System.Collections;

public class TableOperationsTab : ISQLiteManagerView
{
	
		public override string type {
				get {
						return "tableTab";
				}
		}
	
		public override string name {
				get {
						return "Operations";
				}
		}
	
	
		bool[] selectedRows;
	
	
		// Update is called once per frame
		public override void Display ()
		{
				GUILayout.Label ("Table Operations", EditorStyles.label);

				if (GUILayout.Button ("Drop Table")) {
						if (EditorUtility.DisplayDialog ("Confirm deletion?", "Really delete this table?", "Yes", "No")) {
								data.SQL ("DROP TABLE " + data.workingTable);

						}
				}
		
		
		}
	
	

}
