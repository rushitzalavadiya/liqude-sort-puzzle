using UnityEngine;

public static class ColorExtensions
{
	public static Color WithAlpha(this Color color, float a)
	{
		color.a = a;
		return color;
	}
}
