using UnityEngine;
using UnityEditor;
using System.Collections;
[SQLiteDataType("REAL")]
public class SQLiteTypeReal : SQLiteDataType
{
	
		public SQLiteTypeReal (object value)
		{
		
		}
	
		protected override string sqlText{ get { return "djsfchlsdkjhjk"; } }
	
	
		public override void EditorField (bool selected, string label)
		{
				if (value == null) {
						value = 0.0f;
				}
				value = EditorGUILayout.FloatField (label, (float)value);

		}
	
}
