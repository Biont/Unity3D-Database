using UnityEngine;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using Mono.Data.SqliteClient;

public class SQLiteManagerModel
{
		SQLiteDB db; 
		public string workingTable;
		public int page = 0;
		public int itemsPerPage = 10;
		Dictionary<string,TableObject>
				tables = new Dictionary<string, TableObject> ();

		object[] tempRow;
		string[] tableInfo;

		public bool isDirty = false;
	
		public void ClearDB ()
		{
				db = null;
		}
		public bool SetDB (string connectionString)
		{
				SQLiteDB tempDb = new SQLiteDB (connectionString);

				if (tempDb.CheckDB ()) {
						db = tempDb;
						SetDirty ();

						return true;
				}
				return false;
		}
		public bool HasDB ()
		{
				return (db != null);

		}

		public void SetCurrentTable (string table)
		{
				workingTable = table;
				page = 0;
				SetDirty ();
		}

		public void SetDirty ()
		{
				tableInfo = null;
				tables.Clear ();
				tempRow = null;
				isDirty = true;
				ReloadFromDB ();
				GatherTempData ();
				GUI.FocusControl ("");	
		
		}
		void ReloadFromDB ()
		{

				tableInfo = db.TableInfo ();

				foreach (string table in tableInfo) {
						tables.Add (table, new TableObject (db, table));
				}

				isDirty = false;
		}

		public bool IsReady ()
		{ 
//				if (isDirty) {
//						ReloadFromDB ();
//				}
				return (workingTable != null && workingTable != "");
		}

		public string[] GetTypes ()
		{
				if (workingTable == null) {
						Debug.LogError ("No table selected");
						return null;
				}
				return tables [workingTable].types.ToArray ();
		}

		public string[] GetColumnNames ()
		{
				if (workingTable == null) {
						Debug.LogError ("No table selected");
						return null;
				}
				return tables [workingTable].names.ToArray ();
		}

		public object[] GetInsertRow ()
		{
				if (tempRow == null || tempRow.Length != GetTypes ().Length) {
						tempRow = new object[GetTypes ().Length];
				}
				return tempRow;
		}
		public void SetInsertRow (object[] row)
		{
				if (tempRow != null) {  //This check is important because otherwise it is possible to insert an old list even after a table change
						tempRow = row;
				}
		}

		public void Insert (object[] row)
		{
				tables [workingTable].Insert (row);
				SetDirty ();
		}

		public object[][] GetCurrentRows ()
		{
				List<object[]> data = new List<object[]> ();
				foreach (List<object> objects in tables [workingTable].tempData) {
						data.Add (objects.ToArray ());
				}
				return data.ToArray ();
		}
		public string[] GetTableInfo ()
		{
				return tableInfo;
		}

		void GatherTempData ()
		{
				if (IsReady ())
						tables [workingTable].GatherTempData ();

		}

}
