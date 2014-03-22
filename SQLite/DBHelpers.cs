using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

public static class DBHelpers
{


		/// <summary>
		/// concatenates a list of strings into a single string (with a glue).
		/// </summary>
		/// <param name="source">source list</param>
		/// <param name="glue">glue to concatenate the strings</param>
		/// <returns>a single string</returns>
		public static string Implode<T> (this IEnumerable<T> source, string glue, string wrap="")
		{
				// exit criteria
				if (source == null)
						Debug.LogError ("Source cannot be null");
				if (source.Count () <= 0)
						return string.Empty;
		
				// result with first item
				var result = new StringBuilder (wrap + source.First ().ToString () + wrap);
		
				// add glue & item for next items
				foreach (T aPart in source.Skip(1)) {
						result.Append (glue + wrap + aPart.ToString () + wrap);
				}
		
				// return result
				return result.ToString ();
		}




//#if UNITY_EDITOR
//		public static string EditorField (this string input)
//		{
//
//		}
//#endif






}






