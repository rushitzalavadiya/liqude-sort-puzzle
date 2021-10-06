using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPanel : ShowHidable
{
	public struct ViewModel
	{
		public struct Button
		{
			public string Title
			{
				get;
				set;
			}

			public Action Callback
			{
				get;
				set;
			}
		}

		public string Title
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public Button[] Buttons
		{
			get;
			set;
		}
	}

	[SerializeField]
	private Text _titleTxt;

	[SerializeField]
	private Text _messageTxt;

	[SerializeField]
	private List<Button> _buttons = new List<Button>();

	private ViewModel _mViewModel;

	public ViewModel MViewModel
	{
		get
		{
			return _mViewModel;
		}
		set
		{
			if (_buttons.Count < value.Buttons.Length)
			{
				throw new Exception("Too Many Buttons");
			}
			_titleTxt.text = value.Title;
			_messageTxt.text = value.Message;
			for (int j = 0; j < _buttons.Count; j++)
			{
				_buttons[j].gameObject.SetActive(j < value.Buttons.Length);
				if (j < value.Buttons.Length)
				{
					_buttons[j].onClick.RemoveAllListeners();
					int i = j;
					_buttons[j].onClick.AddListener(delegate
					{
						OnClickButton(value.Buttons[i].Callback);
					});
					_buttons[j].GetComponentInChildren<Text>().text = value.Buttons[j].Title;
				}
			}
			_mViewModel = value;
		}
	}

	private void OnClickButton(Action action = null)
	{
		Hide();
		action?.Invoke();
	}

	public override void Show(bool animate = true, Action completed = null)
	{
		base.Show(animate, completed);
	}

	public override void Hide(bool animate = true, Action completed = null)
	{
		base.Hide(animate, completed);
	}

	public void ShowAsConfirmation(string title, string message, Action<bool> callback = null)
	{
		MViewModel = new ViewModel
		{
			Title = title,
			Message = message,
			Buttons = new ViewModel.Button[2]
			{
				new ViewModel.Button
				{
					Title = "No",
					Callback = delegate
					{
						callback?.Invoke(obj: false);
					}
				},
				new ViewModel.Button
				{
					Title = "Yes",
					Callback = delegate
					{
						callback?.Invoke(obj: true);
					}
				}
			}
		};
		Show();
	}

	public void ShowAsInfo(string title, string message, Action onClose = null)
	{
		MViewModel = new ViewModel
		{
			Title = title,
			Message = message,
			Buttons = new ViewModel.Button[1]
			{
				new ViewModel.Button
				{
					Title = "Ok",
					Callback = delegate
					{
						onClose?.Invoke();
					}
				}
			}
		};
		Show();
	}
}
