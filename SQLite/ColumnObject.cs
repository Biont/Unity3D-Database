using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using System.Collections;

public class ColumnObject:ICloneable
{

		public string name = "nullColumn";
		public string type = "null";

		bool hasDefault;
		SQLiteDataType content;
		object _defaultContent;
		SQLiteDataType defaultContent;

		bool notNull = false;
		bool primaryKey = false;
		bool autoIncrement = false;


		private int typeIndex = 0;

		public ColumnObject (string name, string type, bool notNull, object defaultContent, bool primaryKey)
		{
				this.name = name;
				this.type = type;
				this.notNull = notNull;
				this.content = SQLiteDataTypeLoader.GetType (type, defaultContent);
				if (hasDefault = defaultContent != null) {
						this.defaultContent = SQLiteDataTypeLoader.GetType (type, defaultContent);
				} else {

				}
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
		public void EditorFieldSetup (string[] types, bool editable=true)
		{
				string oldType = type;

				GUI.enabled = editable;

				name = EditorGUILayout.TextField (name);
				type = types [typeIndex = EditorGUILayout.Popup (typeIndex, types)];
				primaryKey = EditorGUILayout.Toggle ("Primary Key", primaryKey);
				autoIncrement = EditorGUILayout.Toggle ("AutoIncrement", autoIncrement);
				notNull = EditorGUILayout.Toggle ("Not null", notNull);

				if (defaultContent == null || oldType != type) {
						defaultContent = SQLiteDataTypeLoader.GetType (type, null);
				}

				if (hasDefault = EditorGUILayout.Toggle ("Default", hasDefault)) {
						defaultContent.EditorField (false, "");
				}

				GUI.enabled = true;
		}

		public void SetValue (object value)
		{
				content.SetValue (value);
		}

		public override string ToString ()
		{
		
				StringBuilder sb = new StringBuilder (string.Format ("'{0}' {1}", name, type));

				if (primaryKey)
						sb.Append (" PRIMARY KEY");
				if (notNull)
						sb.Append (" NOT NULL");
				if (hasDefault)
						sb.AppendFormat (" DEFAULT('{0}')", defaultContent.ToString ());


				return sb.ToString ();
		}

		public object Clone ()
		{
				return new ColumnObject (name, type, notNull, _defaultContent, primaryKey);
		}


}
