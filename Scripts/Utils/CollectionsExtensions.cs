using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
	public static class CollectionsExtensions
	{
		public static T Random<T>(this List<T> list, Func<int, int, int> randomFunc)
		{
			return list[randomFunc(0, list.Count)];
		}

		public static void SafeAdd<TKey, TSource>(this Dictionary<TKey, TSource> dictionary, TKey key, TSource value)
		{
			if (dictionary.ContainsKey(key))
				return;
			dictionary.Add(key, value);
		}

		public static void ForEachIdx<T>(this IEnumerable<T> collection, Action<T, int> action)
		{
			var list = collection.ToList();
			for (var i = 0; i < list.Count; i++)
				action(list[i], i);
		}
	}
}