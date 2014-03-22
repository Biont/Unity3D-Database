using UnityEngine;
using UnityEditor;
using System.Collections;

[SQLiteDataType("INTEGER")]
public class SQLiteTypeInteger : SQLiteDataType
{

		public SQLiteTypeInteger (object value)
		{
		
		}
		protected override string sqlText{ get { return "djsfchlsdkjhjk"; } }
	
	
		public override void EditorField (bool selected, string label)
		{
				value = EditorGUILayout.IntField (label, (int)value);
		}

}
