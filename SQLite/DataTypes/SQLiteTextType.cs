using UnityEngine;
using UnityEditor;
using System.Collections;

public class SQLiteTextType : SQLiteDataType
{

		public string fieldName{ get { return "TEXT"; } }
		public string sqlText{ get { return "djsfchlsdkjhjk"; } }

		string data;

		public void EditorField (string label)
		{
				data = EditorGUILayout.TextField (label, data);
		}

}
