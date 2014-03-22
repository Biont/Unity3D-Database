using UnityEngine;
using UnityEditor;
using System.Collections;
[SQLiteDataType("VECTOR3")]
public class Vector3SQLiteDataType : SQLiteDataType
{
	
		public Vector3SQLiteDataType (object value)
		{
		
		}
	
		protected override string sqlText{ get { return "djsfchlsdkjhjk"; } }
	
	
		public override void EditorField (bool selected, string label)
		{
				if (value == null) {
						value = Vector3.zero;
				}
		
				value = EditorGUILayout.Vector3Field (label, (Vector3)value);
		
		}
	
}
