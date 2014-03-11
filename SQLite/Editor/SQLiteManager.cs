using UnityEngine;
using UnityEditor;

public class SQLiteManager : EditorWindow
{
		[MenuItem ("Window/My Window")]
		static void Init ()
		{
				SQLiteManager window = (SQLiteManager)EditorWindow.GetWindow (typeof(SQLiteManager));
		}


		SQLiteDB db;
		string workingTable = null;

		void OnGUI ()
		{

				if (db == null) {

						DBSelector ();

				} else {
						GUILayout.BeginArea (new Rect (0, 0, 100, 100));
						AvailableTables ();
						GUILayout.EndArea ();

						GUILayout.BeginArea (new Rect (100, 0, 100, 100));
						AvailableColumns ();
						GUILayout.EndArea ();
			
				}
		
		}


		void DBSelector ()
		{
				Object source = null;
				source = EditorGUILayout.ObjectField (source, typeof(Object), true);
				if (source != null) {
						string workingDB = Application.dataPath + "/../" + AssetDatabase.GetAssetPath (source);
						db = new SQLiteDB (workingDB);

						if (!db.CheckDB ()) {
								Debug.Log ("This is not a valid SQLite Database");
								db = null;
						}
			
				}

				//TODO "Or create a new Database"
		}

		void AvailableTables ()
		{
				GUILayout.Label ("DB Selected", EditorStyles.boldLabel);
				foreach (string table in db.TableInfo ()) {
						if (GUILayout.Button (table)) {
								workingTable = table;
						}
			
				}
		}

		void AvailableColumns ()
		{
				if (workingTable != null) {
						GUILayout.Label ("Selected table " + workingTable, EditorStyles.boldLabel);
				} else {
						GUILayout.Label ("Select a table from the left", EditorStyles.boldLabel);
			
				}
		
		}
	
}
