using UnityEngine;
using UnityEditor;
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

		public bool _hasChanged = false;
		public bool hasChanged {
				get {
						if (_hasChanged) {
								Debug.Log ("hasChanged");
				
								_hasChanged = false;
								return true;
						}
						return false;
				}
		}

		Dictionary<string,TableObject>
				tables = new Dictionary<string, TableObject> ();

		object[] tempRow;
		string[] tableInfo;

		public int rowCount{ get { return tables [workingTable].rowCount; } }

		public bool isReady{ get { return (workingTable != null && workingTable != ""); } }
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
				if (workingTable != table) {
						workingTable = table;
						page = 0;
						SetDirty ();
				}
		}

		public void SetRange (int page, int itemsPerPage)
		{
				bool reload = false;
				if (this.page != page || this.itemsPerPage != itemsPerPage) {
						Debug.Log ("Range hat sich geändert");
						reload = true;
				}
				this.page = page;
				this.itemsPerPage = itemsPerPage;
				if (reload) {
						GatherTempData ();
						_hasChanged = true;
				}
			
		}
		void SetDirty ()
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
				Debug.Log ("rows in tempdata of current table: " + tables [workingTable].tempData.Count);
				foreach (List<ColumnObject> objects in tables [workingTable].tempData) {
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
				if (isReady) {
						tables [workingTable].GatherTempData (page, itemsPerPage);
				}

		}

		public List<ColumnObject> GetTableLayout ()
		{
				return tables [workingTable].GetTablelayout ();
		}

}
