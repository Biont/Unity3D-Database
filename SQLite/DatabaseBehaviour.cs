using UnityEngine;
using System.Collections;

public class DatabaseBehaviour : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				Debug.Log (Application.persistentDataPath);
				SQLiteDB db = new SQLiteDB (Application.dataPath + "/huhu");
				db.Query ("CREATE TABLE test (name VARCHAR(20), score INT)");
				db.Query ("CREATE TABLE test2 (label TEXT, position REAL)");
		}
	

}
