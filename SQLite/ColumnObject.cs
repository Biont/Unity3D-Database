using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class ColumnObject:ICloneable
{

		public string name = "nullColumn";
		public string type = "null";

		SQLiteDataType content;
		object _defaultContent;
		SQLiteDataType defaultContent;

		bool notNull = false;
		bool primaryKey = false;
		bool autoIncrement = false;


		public ColumnObject (string name, string type, bool notNull, object defaultContent, bool primaryKey)
		{
				this.name = name;
				this.type = type;
				this.notNull = notNull;
				this.content = SQLiteDataTypeLoader.GetType (type, defaultContent);
				this.defaultContent = SQLiteDataTypeLoader.GetType (type, defaultContent);
				this.primaryKey = primaryKey;


		}

		/// <summary>
		/// Shows the controls for editing the value of a column
		/// </summary>
		/// <param name="selected">If set to <c>true</c> selected.</param>
		/// <param name="label">Label.</param>
		public void EditorField (bool selected, string label="")
		{
				GUILayout.BeginVertical ();
				if (selected) {
						GUILayout.BeginHorizontal ();
						if (primaryKey) {
								GUILayout.Label ("Primary Key", EditorStyles.boldLabel);
						}
						GUI.enabled = false;
						GUILayout.Toggle (notNull, "Not null");
						GUI.enabled = true;
						GUILayout.EndHorizontal ();
				}
				content.EditorField (selected, label);
				GUILayout.EndVertical ();
		}

		/// <summary>
		/// This show the controls for adding or changing a column
		/// </summary>
		/// <param name="editable">If set to <c>true</c> editable.</param>
		public void EditorFieldSetup (bool editable=true)
		{
				GUI.enabled = editable;

				primaryKey = EditorGUILayout.Toggle ("Primary Key", primaryKey);
				autoIncrement = EditorGUILayout.Toggle ("AutoIncrement", autoIncrement);
				notNull = EditorGUILayout.Toggle ("Not null", notNull);


				GUI.enabled = true;
		}

		public void SetValue (object value)
		{
				content.SetValue (value);
		}

		public object Clone ()
		{
				return new ColumnObject (name, type, notNull, _defaultContent, primaryKey);
		}


}
