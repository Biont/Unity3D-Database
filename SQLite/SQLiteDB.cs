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
				command.CommandText = sql;
				command.ExecuteNonQuery ();
		}

		public IDataReader Fetch (string sql)
		{
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



}
