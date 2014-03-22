using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

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

		private int[] typeIndices;

		public override void Display ()
		{
				GUILayout.Label ("Edit table layout", EditorStyles.label);
		
				if (data.isReady) {
						int numCols = data.tableLayout.Length;
			
						GetTypeIndices ();
			
						//Top row
						for (int i=0; i<numCols; i++) {
								EditorGUILayout.BeginHorizontal ();
								data.tableLayout [i].name = EditorGUILayout.TextField (data.tableLayout [i].name);
								typeIndices [i] = EditorGUILayout.Popup (typeIndices [i], data.availableTypes);

								EditorGUILayout.EndHorizontal ();
				
						}
						// Fetched rows

						if (GUILayout.Button ("Reset")) {
								typeIndices = null;
						}
						if (GUILayout.Button ("Save")) {
						}
			
			
				} else {
						GUILayout.Label ("Select a table from the left", EditorStyles.boldLabel);
			
				}
		
		}

		private void GetTypeIndices ()
		{
				int numCols = data.tableLayout.Length;
				if (typeIndices == null || typeIndices.Length != numCols) {
						typeIndices = new int[data.tableLayout.Length];
						for (int i=0; i<numCols; i++) {
								typeIndices [i] = Array.IndexOf (data.availableTypes, data.tableLayout [i].type);
						}
				}
		}



}

