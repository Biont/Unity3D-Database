using UnityEngine;
using UnityEditor;
using System.Collections;

public class TableLayoutView : ISQLiteManagerView
{

		public override string type {
				get {
						return "tableTab";
				}
		}
	
		public override string name {
				get {
						return "Layout";
				}
		}


		public override void Display ()
		{
				GUILayout.Label ("Edit table layout", EditorStyles.label);
		
		}



}

