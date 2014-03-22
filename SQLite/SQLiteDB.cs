using UnityEngine;
using System.Data;
using System.Collections.Generic;
using Mono.Data.SqliteClient;


public class SQLiteDB
{

		private IDbConnection   connection;
		private IDbCommand  command;



		public SQLiteDB (string path)
		{
				string connectionString = "URI=file:" + path;
			


				connection = new SqliteConnection (connectionString);
		
				connection.Open ();
				command = connection.CreateCommand ();

		}


		public void Query (string sql)
		{
				Debug.Log (sql);
				command.CommandText = sql;
				command.ExecuteNonQuery ();
		}

		public IDataReader Fetch (string sql)
		{
//				Debug.Log (sql);
				command.CommandText = sql;
				return command.ExecuteReader ();


		}

		public bool CheckDB ()
		{
				try {
						IDataReader info = Fetch ("SELECT * FROM sqlite_master");
				} catch (SqliteSyntaxException) {
						return false;
				}
				return true;
		   
		}

		public string[] TableInfo ()
		{
				IDataReader info = Fetch ("SELECT * FROM sqlite_master WHERE type='table'");
				List<string> tables = new List<string> ();
				while (info.Read ()) {
						tables.Add ((string)info ["tbl_name"]);
				}

				return tables.ToArray ();

		}

		public List<ColumnObject> ColumnInfo (string tableName)
		{
				IDataReader info = Fetch ("PRAGMA table_info(" + tableName + ");");
				List<ColumnObject> columns = new List<ColumnObject> ();




				while (info.Read ()) {
						columns.Add (new ColumnObject (
				info.GetString (1), //Name
				GetSQLType (info.GetString (2)), //Type
				(info.GetInt64 (3) > 0),//NOTNULL
				info.GetValue (4),//Default
			(info.GetInt64 (5) > 0)//PrimaryKey
						));
				}
				
		
				return columns;
		}


	

		/// <summary>
		/// Gets the type of a field type. It's a safety net against usage of mysql field types,
		/// so we know we're only dealing with sqlite types
		/// </summary>
		/// <returns>The SQL type.</returns>
		/// <param name="input">Input.</param>
		private string GetSQLType (string input)
		{
				if (input.Contains ("CHAR")) {
						return "TEXT";
				}
				if (input.Contains ("TEXT")) {
						return "TEXT";
				}



				if (input.Contains ("INT")) {
						return "INTEGER";
				}

				if (input.Contains ("BLOB")) {
						return "BLOB";
				}

				return input;
		}



}
