using UnityEngine;
using System.Collections;

public abstract class ISQLiteManagerView
{
		public virtual string type{ get {
						return "ISQLITEMANAGER";
				} }
	
		public virtual string name{ get {
						return "ISQLITEMANAGER";
				} }

		protected SQLiteManager data;

		public void Prepare (SQLiteManager data)
		{
				this.data = data;
				Display ();
		}

		public abstract void Display ();

}


