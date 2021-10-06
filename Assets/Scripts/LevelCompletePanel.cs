using Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : ShowHidable
{
	[SerializeField]
	private Text _toastTxt;

	[SerializeField]
	private List<string> _toasts = new List<string>();

	protected override void OnShowCompleted()
	{
		base.OnShowCompleted();
		_toastTxt.text = _toasts.GetRandom();
		_toastTxt.gameObject.SetActive(value: true);
	//	AdsManager.ShowOrPassAdsIfCan();
	}

	public void OnClickContinue()
	{
		UIManager.Instance.LoadNextLevel();
	}
}
