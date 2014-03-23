using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

public static class DBHelpers
{


		/// <summary>
		/// concatenates a list of strings into a single string (with a glue).
		/// </summary>
		/// <param name="source">source list</param>
		/// <param name="glue">glue to concatenate the strings</param>
		/// <returns>a single string</returns>
		public static string Implode<T> (this IEnumerable<T> source, string glue, string wrap="")
		{
				// exit criteria
				if (source == null)
						Debug.LogError ("Source cannot be null");
				if (source.Count () <= 0)
						return string.Empty;
		
				// result with first item
				var result = new StringBuilder (wrap + source.First ().ToString () + wrap);
		
				// add glue & item for next items
				foreach (T aPart in source.Skip(1)) {
						result.Append (glue + wrap + aPart.ToString () + wrap);
				}
		
				// return result
				return result.ToString ();
		}






//#if UNITY_EDITOR
//		public static string EditorField (this string input)
//		{
//
//		}
//#endif






}

namespace UnityEditor
{

		public static class BiontEditorExtensions
		{



				/// <summary>
				/// Lists the tool.
				/// </summary>
				/// <param name="list">List.</param>
				/// <param name="eachItemAction">Each item action.</param>
				/// <param name="addAction">Add action.</param>
				/// <typeparam name="T">The 1st type parameter.</typeparam>
				public static void ListTool<T> (this IList<T> list, System.Action<T> eachItemAction, System.Func<T> addAction=null)
				{

						int? remove = null;
						int count = list.Count;
						int i = 0;
						foreach (T item in list) {
								EditorGUILayout.BeginHorizontal ();
								GUI.enabled = (i > 0);
								if (GUILayout.Button ("+", GUILayout.Width (20))) {
										GUI.FocusControl ("");
										list.MoveUp (i);
								}
								GUI.enabled = true;
								GUI.enabled = (i < count - 1);
				
								if (GUILayout.Button ("-", GUILayout.Width (20))) {
										GUI.FocusControl ("");
										list.MoveDown (i);
								}
								GUI.enabled = true;
				
								if (GUILayout.Button ("x", GUILayout.Width (20))) {
										remove = i;
								}
								eachItemAction (item);

								EditorGUILayout.EndHorizontal ();
								i++;
						}
						if (addAction != null) {
								if (GUILayout.Button ("Add Item")) {
										list.Add (addAction ());
								}
						}

						if (remove != null) {
								list.RemoveAt (remove.Value);
				
						}

				}




				public static void MoveUp<T> (this IList<T> list, int iIndexToMove)
				{
		
						if (iIndexToMove > 0) {
								var old = list [iIndexToMove - 1];
								list [iIndexToMove - 1] = list [iIndexToMove];
								list [iIndexToMove] = old;
						}
				}
		
				public static void MoveDown<T> (this IList<T> list, int iIndexToMove)
				{
			
						if (iIndexToMove < list.Count - 1) {
								
								var old = list [iIndexToMove + 1];
								list [iIndexToMove + 1] = list [iIndexToMove];
								list [iIndexToMove] = old;
						
						}
				}

		}
		
}




