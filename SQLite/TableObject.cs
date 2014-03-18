using UnityEngine;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using Mono.Data.SqliteClient;


public class TableObject
{
		SQLiteDB db; 
		public string workingTable;
		public int page = 0;
		public int itemsPerPage = 10;

		public List<string> names;
		public List<string> types;
		public List<List<object>> tempData = new List<List<object>> ();



		public TableObject (SQLiteDB db, string table)
		{
				this.db = db;
				workingTable = table;

				names = new List<string> ();
				types = new List<string> ();
		
			
				object[][] colInfo = db.ColumnInfo (workingTable);
			
				foreach (object[] column in colInfo) {
						names.Add ((string)column [0]);
						types.Add ((string)column [1]);
				
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

		public void GatherTempData ()
		{
				if (workingTable != null && workingTable != "") {
			
						int offset = page * itemsPerPage;
			
						IDataReader r = db.Fetch ("SELECT * FROM " + workingTable + " LIMIT " + itemsPerPage + " OFFSET " + offset + ";");
			
						int index = 0;
						while (r.Read()) {
								List<object> newList = new List<object> ();
				
								for (int i = 0; i < r.FieldCount; i ++) {
										newList.Add (r.GetValue (i));
								}  
								index++;
								tempData.Add (newList);
				
						}
				}
		
		}
}
