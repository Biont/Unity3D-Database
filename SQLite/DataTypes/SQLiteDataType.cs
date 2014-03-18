using UnityEngine;
using System.Collections;

public interface SQLiteDataType
{
		string fieldName{ get; }
		string sqlText{ get; }



		void EditorField (string label);



}
