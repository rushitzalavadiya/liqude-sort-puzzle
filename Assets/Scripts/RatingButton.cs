using UnityEngine;
using UnityEngine.EventSystems;

public class RatingButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public static bool Rated
	{
		get
		{
			return PrefManager.GetBool("Rated");
		}
		private set
		{
			PrefManager.SetBool("Rated", value);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OpenUrl();
	}

	public static void OpenUrl()
	{
		Rated = true;
	}
}
