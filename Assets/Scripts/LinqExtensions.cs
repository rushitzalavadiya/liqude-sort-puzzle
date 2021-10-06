using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LinqExtensions
{
	public static void AddOrUpdate<T, TJ>(this IDictionary<T, TJ> dict, T key, TJ val)
	{
		if (dict.ContainsKey(key))
		{
			dict[key] = val;
		}
		else
		{
			dict.Add(key, val);
		}
	}

	public static TJ GetOrDefault<T, TJ>(this IDictionary<T, TJ> dict, T key)
	{
		if (!dict.ContainsKey(key))
		{
			return default(TJ);
		}
		return dict[key];
	}

	public static T GetRandom<T>(this IEnumerable<T> enumerable, out int index)
	{
		List<T> list = enumerable.ToList();
		index = UnityEngine.Random.Range(0, list.Count);
		return list[index];
	}

	public static T GetRandomWithReduceFactor<T>(this IEnumerable<T> enumerable, float factor)
	{
		List<T> list = enumerable.ToList();
		List<float> list2 = new List<float>();
		float num = 1f;
		list2.Add(1f);
		for (int i = 1; i < list.Count; i++)
		{
			num *= factor;
			list2.Add(num);
		}
		float num2 = UnityEngine.Random.Range(0f, list2.Sum());
		for (int j = 0; j < list.Count; j++)
		{
			num2 -= list2[j];
			if (num2 <= 0f)
			{
				return list[j];
			}
		}
		return list.GetRandom();
	}

	public static T GetRandom<T>(this IEnumerable<T> enumerable)
	{
		int index;
		return enumerable.GetRandom(out index);
	}

	public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> enumerable, int count)
	{
		List<T> list = enumerable.ToList();
		if (list.Count < count)
		{
			throw new InvalidOperationException();
		}
		for (int i = 0; i < count; i++)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			yield return list[index];
			list.RemoveAt(index);
		}
	}

	public static T GetRandomOrDefault<T>(this IEnumerable<T> enumerable)
	{
		List<T> list = enumerable.ToList();
		if (list.Count == 0)
		{
			return default(T);
		}
		return list.GetRandom();
	}

	public static T GetAndRemove<T>(this IList<T> list, T item)
	{
		return list.GetAndRemove(list.IndexOf(item));
	}

	public static T GetAndRemove<T>(this IList<T> list, int index)
	{
		if (index < 0 || index >= list.Count)
		{
			return default(T);
		}
		T result = list[index];
		list.RemoveAt(index);
		return result;
	}

	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
	{
		foreach (T item in enumerable)
		{
			action?.Invoke(item);
		}
	}
}
