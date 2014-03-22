﻿using UnityEngine;
using UnityEditor;
using System.Collections;
[SQLiteDataType("TEXT")]
public class SQLiteTypeText : SQLiteDataType
{

		public SQLiteTypeText (object value)
		{

		}

		protected override string sqlText{ get { return "djsfchlsdkjhjk"; } }


		public override void EditorField (bool selected, string label)
		{
				if (value == null) {
						value = "";
				}
		
				value = EditorGUILayout.TextField (label, (string)value);
		
		}

}
