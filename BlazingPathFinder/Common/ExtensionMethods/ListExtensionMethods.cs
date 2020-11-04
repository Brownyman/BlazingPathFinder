using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazingPathFinder.Common.ExtensionMethods
{
	public static class ListExtensionMethods
	{
		/// <summary>
		/// Returns and Removes first item in the List.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static T RemoveAndReturnFirst<T>(this List<T> list)
		{
			if(list == null || list.Count == 0)
			{
				// Instead of return the default,
				// and exception might be more compliant to the method signature.

				return default(T);
			}

			T currentFirst = list[0];
			list.RemoveAt(0);
			return currentFirst;
		}
	}
}
