using UnityEngine;
using UnityEditor;
using System.Collections;

[SQLiteDataType("TEXTURE2D")]
public class Texture2DSQLiteType : SQLiteDataType
{

		public Texture2DSQLiteType (object value)
		{
		
		}
		protected override string sqlText{ get { return "djsfchlsdkjhjk"; } }
	
	
		public override void EditorField (bool selected, string label)
		{
//				if (value == null) {
//						value = null;
//				}
				value = EditorGUILayout.ObjectField (value as Texture2D, typeof(Texture2D));
		}
}
