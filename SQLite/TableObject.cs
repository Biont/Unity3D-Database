using UnityEngine;
using System.Collections;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Mono.Data.SqliteClient;


public class TableObject
{
		SQLiteDB db; 
		public string workingTable;
		public int rowCount;
		public List<string> names;
		public List<string> types;

		List<ColumnObject> columns;

		public List<List<ColumnObject>> tempData = new List<List<ColumnObject>> ();



		public TableObject (SQLiteDB db, string table)
		{
				this.db = db;
				workingTable = table;

				names = new List<string> ();
				types = new List<string> ();

				columns = this.db.ColumnInfo (workingTable);
			
				GetRowCount ();
				foreach (ColumnObject column in columns) {
						names.Add (column.name);
						types.Add (column.type);
				
				}
		
		}

		private void GetRowCount ()
		{
				IDataReader r = this.db.Fetch ("SELECT COUNT(*) FROM " + workingTable);
				while (r.Read ()) {
						rowCount = r.GetInt32 (0);
				}

		}
	
		public void GetData (int page, int itemsPerPage)
		{

		}

		public void Insert (object[] row)
		{
				string sql = "INSERT INTO " + workingTable + " (" + names.Implode (",", "'") + ") VALUES (" + row.Implode (",", "'") + ");";
				db.Query (sql);
		}

		public void GatherTempData (int page, int itemsPerPage)
		{
				tempData.Clear ();
				if (workingTable != null && workingTable != "") {
			
						int offset = page * itemsPerPage;
			
						IDataReader r = db.Fetch ("SELECT * FROM " + workingTable + " LIMIT " + itemsPerPage + " OFFSET " + offset + ";");
			
						int index = 0;
						while (r.Read()) {
								List<ColumnObject> newList = new List<ColumnObject> (columns.Select (x => x.Clone ()as ColumnObject));
				
								for (int i = 0; i < r.FieldCount; i ++) {
										newList [i].SetValue (r.GetValue (i));
//										Debug.Log (r.GetValue (i).ToString ());
								}  
								index++;
								tempData.Add (newList);
				
						}
				}
		
		}

		public List<ColumnObject> GetTablelayout ()
		{
				if (columns == null) {
						Debug.Log ("No columns :(");
				}
				return columns;
		}
}
