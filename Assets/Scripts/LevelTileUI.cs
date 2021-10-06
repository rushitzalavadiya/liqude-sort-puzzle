using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelTileUI : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public struct ViewModel
	{
		public Level Level
		{
			get;
			set;
		}

		public bool Locked
		{
			get;
			set;
		}

		public bool Completed
		{
			get;
			set;
		}
	}

	[SerializeField]
	private Text _txt;

	[SerializeField]
	private GameObject _completeMark;

	[SerializeField]
	private GameObject _lockMark;

	[SerializeField]
	private Image _fillImg;

	private ViewModel _mViewModel;

	public ViewModel MViewModel
	{
		get
		{
			return _mViewModel;
		}
		set
		{
			Text txt = _txt;
			Level level = value.Level;
			txt.text = level.no.ToString();
			_fillImg.color = _fillImg.color.WithAlpha((!value.Locked) ? 1 : 0);
			txt.gameObject.SetActive(!value.Locked);
			_completeMark.SetActive(value.Completed);
			_lockMark.SetActive(value.Locked);
			_mViewModel = value;
		}
	}

	public event Action<LevelTileUI> Clicked;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		this.Clicked?.Invoke(this);
	}
}
