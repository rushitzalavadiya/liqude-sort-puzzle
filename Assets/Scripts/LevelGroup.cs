using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct LevelGroup : IEnumerable<Level>, IEnumerable
{
	public List<Level> levels;

	public IEnumerator<Level> GetEnumerator()
	{
		List<Level>.Enumerator? enumerator = levels?.GetEnumerator();
		if (!enumerator.HasValue)
		{
			return Enumerable.Empty<Level>().GetEnumerator();
		}
		return enumerator.GetValueOrDefault();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
