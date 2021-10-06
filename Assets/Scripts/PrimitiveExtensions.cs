using UnityEngine;

public static class PrimitiveExtensions
{
	public static int FloorTo(this int value, int digit)
	{
		int num = (int)Mathf.Pow(10f, digit);
		return value / num * num;
	}
}
