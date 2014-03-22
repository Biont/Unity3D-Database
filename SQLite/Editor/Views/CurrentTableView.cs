using UnityEngine;
using UnityEditor;
using System.Collections;

public class CurrentTableView : ISQLiteManagerView
{
	#region ISQLiteManagerView Initialization
		public override string type {
				get {
						return "screen";
				}
		}
	
		public override string name {
				get {
						return "CurrentTable";
				}
		}
	#endregion



		private string selectedTab = "";

		public override void Display ()
		{
				if (data.isReady) {
						GUILayout.Label ("Current Table", EditorStyles.label);
						TableView ();
				} else {
						GUILayout.Label ("No Table selected", EditorStyles.label);
			
				}
		}


		void TableView ()
		{
				TableViewTabs ();
				data.ShowView ("tableTab", selectedTab);
		
		}

		void TableViewTabs ()
		{
				EditorGUILayout.BeginHorizontal ();
				foreach (string tab in data.GetViewNames("tableTab")) {
						GUI.enabled = (tab != selectedTab);
						if (GUILayout.Button (tab)) {
								selectedTab = tab;
						}
						GUI.enabled = true;
			
				}
				EditorGUILayout.EndHorizontal ();
		
		}


		
}
