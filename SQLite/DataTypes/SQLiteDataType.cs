using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;





[SQLiteDataType(null)]
public  class SQLiteDataType
{

		protected virtual string  sqlText{ get { return ""; } }

		public object value;

		public SQLiteDataType (object value=null)
		{

		}


		public virtual void EditorField (bool selected, string label)
		{
				GUILayout.Label ("Unrecognized Type", EditorStyles.boldLabel);
		
		}

		public virtual void SetValue (object value)
		{
				this.value = value;
		}
		public object GetValue ()
		{
				return value;
		}



}


public static class SQLiteDataTypeLoader
{


		private static Dictionary<string,System.Type> _typeDict;

		public static Dictionary<string,System.Type> GetDataTypes ()
		{
				if (_typeDict == null) {
						Dictionary<string,System.Type> typeDict = new Dictionary<string,System.Type> ();

						var baseType = typeof(SQLiteDataType);
						var assembly = baseType.Assembly;
						IEnumerable<System.Type> types = assembly.GetTypes ().Where (t => t.IsSubclassOf (baseType));

						foreach (System.Type type in types) {

								object[] atts = type.GetCustomAttributes (typeof(SQLiteDataTypeAttribute), false);

								if (atts.Length == 0)
										continue;

								SQLiteDataTypeAttribute dataType = (SQLiteDataTypeAttribute)atts [0];

								typeDict.Add (dataType.type, type);
						}
						_typeDict = typeDict;
				}
				return _typeDict;
		}

		public static SQLiteDataType GetType (string typeString, object value)
		{

				Dictionary<string,System.Type> types = SQLiteDataTypeLoader.GetDataTypes ();

				if (types.ContainsKey (typeString)) {

						return  Activator.CreateInstance (types [typeString], value) as SQLiteDataType;
				} else {
						Debug.Log ("Datatype not found: " + typeString);
						return  new SQLiteDataType (value);
						
				}

		}



}

[System.AttributeUsage(System.AttributeTargets.Class)]
public class SQLiteDataTypeAttribute : System.Attribute
{
		private string _type;
	
		public SQLiteDataTypeAttribute (string type)
		{
				this._type = type;
		}

		public string type
		{ get { return _type; } }
}





