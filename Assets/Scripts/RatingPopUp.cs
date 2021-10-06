using MyGame;

public class RatingPopUp : ShowHidable
{
	public const int MIN_GAME_COUNT_AT_START = 5;

	public const int MIN_GAME_COUNT_AFTER_LATER = 50;

	private static int NEXT_MIN_COUNT
	{
		get
		{
			return PrefManager.GetInt("Rating_NEXT_MIN_COUNT", 5);
		}
		set
		{
			PrefManager.SetInt("Rating_NEXT_MIN_COUNT", value);
		}
	}

	public static bool Available
	{
		get
		{
			if (!RatingButton.Rated)
			{
				return NEXT_MIN_COUNT < GameManager.TOTAL_GAME_COUNT;
			}
			return false;
		}
	}

	public void OnClickRate()
	{
		RatingButton.OpenUrl();
		Hide();
	}

	public void OnClickLater()
	{
		NEXT_MIN_COUNT = GameManager.TOTAL_GAME_COUNT + 50;
		Hide();
	}
}
