using UnityEngine;

public static class PrefManager
{
	public static void SetInt(string key, int val)
	{
		PlayerPrefs.SetInt(key, val);
	}

	public static int GetInt(string key, int defVal = 0)
	{
		return PlayerPrefs.GetInt(key, defVal);
	}

	public static void SetString(string key, string val)
	{
		PlayerPrefs.SetString(key, val);
	}

	public static string GetString(string key, string def = "")
	{
		return PlayerPrefs.GetString(key, def);
	}

	public static void Clear()
	{
		PlayerPrefs.DeleteAll();
	}

	public static bool HasKey(string key)
	{
		return PlayerPrefs.HasKey(key);
	}

	public static bool GetBool(string key, bool def = false)
	{
		return GetInt(key, def ? 1 : 0) == 1;
	}

	public static void SetBool(string key, bool val)
	{
		SetInt(key, val ? 1 : 0);
	}
}
