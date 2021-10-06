using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct LevelColumn : IEnumerable<int>, IEnumerable
{
	public List<int> values;

	public IEnumerator<int> GetEnumerator()
	{
		List<int>.Enumerator? enumerator = values?.GetEnumerator();
		if (!enumerator.HasValue)
		{
			return Enumerable.Empty<int>().GetEnumerator();
		}
		return enumerator.GetValueOrDefault();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
