using System;

public interface IShowHideable
{
	bool Showing
	{
		get;
	}

	ShowState CurrentShowState
	{
		get;
	}

	event EventHandler<bool> ShowStateChanged;

	void Show(bool animate = true, Action onFinished = null);

	void Hide(bool animate = true, Action onFinished = null);
}
