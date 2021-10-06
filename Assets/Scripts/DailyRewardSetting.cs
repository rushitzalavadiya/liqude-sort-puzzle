using System;
using System.Collections.Generic;

[Serializable]
public struct DailyRewardSetting
{
	public bool enable;

	public List<int> rewards;
}
