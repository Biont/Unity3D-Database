using UnityEngine;
using UnityEditor;
using System.Collections;

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
	
		// Update is called once per frame
		public override void Display ()
		{
				GUILayout.Label ("createtableview", EditorStyles.label);
	
		}
}
