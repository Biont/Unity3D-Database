using UnityEngine;
using UnityEditor;
using System.Collections;

public class SettingsScreenView : ISQLiteManagerView
{

	#region ISQLiteManagerView Initialization
		public override string type {
				get {
						return "screen";
				}
		}
	
		public override string name {
				get {
						return "Settings";
				}
		}
	#endregion


		public override void Display ()
		{
				GUILayout.Label ("Edit settings", EditorStyles.label);
				data.rowsPerPage = EditorGUILayout.IntField ("Rows per page", data.rowsPerPage);
		}


}
