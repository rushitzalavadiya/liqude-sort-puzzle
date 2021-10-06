using UnityEngine;

public class SharedUIManager : Singleton<SharedUIManager>
{
	[SerializeField]
	private LoadingPanel _loadingPanel;

	[SerializeField]
	private RatingPopUp _ratingPopUp;

	[SerializeField]
	private PopUpPanel _popUpPanel;

	// [SerializeField]
	// private ConsentPanel _consentPanel;
	//
	// public static ConsentPanel ConsentPanel => Singleton<SharedUIManager>.Instance._consentPanel;

	public static PopUpPanel PopUpPanel => Singleton<SharedUIManager>.Instance._popUpPanel;

	public static LoadingPanel LoadingPanel => Singleton<SharedUIManager>.Instance?._loadingPanel;

	public static RatingPopUp RatingPopUp => Singleton<SharedUIManager>.Instance?._ratingPopUp;
}
