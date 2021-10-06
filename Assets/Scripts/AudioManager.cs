using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	public static bool IsSoundEnable
	{
		get
		{
			return PlayerPrefs.GetInt("IsSoundEnable", 1) == 1;
		}
		set
		{
			if (value != IsSoundEnable)
			{
				PlayerPrefs.SetInt("IsSoundEnable", value ? 1 : 0);
				AudioManager.SoundStateChanged?.Invoke(value);
			}
		}
	}

	public static event Action<bool> SoundStateChanged;
}
