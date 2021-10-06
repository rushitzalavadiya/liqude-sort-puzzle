using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct Level
{
	public int no;

	public List<LevelColumn> map;

	public List<IEnumerable<LiquidData>> LiquidDataMap => map.Select(GetLiquidDatas).ToList();

	public static IEnumerable<LiquidData> GetLiquidDatas(LevelColumn column)
	{
		List<int> list = column.ToList();
		for (int i = 0; i < list.Count; i++)
		{
			int num = list[i];
			int num2 = 0;
			for (; i < list.Count; i++)
			{
				if (num == list[i])
				{
					num2++;
					continue;
				}
				i--;
				break;
			}
			yield return new LiquidData
			{
				groupId = num,
				value = num2
			};
		}
	}
}
