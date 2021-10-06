using System;
using System.Collections.Generic;
using UnityEngine;

public class ShowHidable : MonoBehaviour, IShowHideable
{
	protected static readonly int SHOW_HASH = Animator.StringToHash("Show");

	protected static readonly int HIDE_HASH = Animator.StringToHash("Hide");

	[SerializeField]
	protected Animator anim;

	[SerializeField]
	protected List<GameObject> dependObjects = new List<GameObject>();

	protected ShowState currentShowState = ShowState.Hide;

	public bool Showing
	{
		get
		{
			return base.gameObject.activeSelf;
		}
		protected set
		{
			if (value != base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(value);
				this.ShowStateChanged?.Invoke(this, value);
			}
		}
	}

	public ShowState CurrentShowState
	{
		get
		{
			return currentShowState;
		}
		protected set
		{
			if (currentShowState != value)
			{
				if (!Showing && (value == ShowState.ShowAnimation || value == ShowState.Show))
				{
					Showing = true;
				}
				else if (Showing && value == ShowState.Hide)
				{
					Showing = false;
				}
				currentShowState = value;
			}
		}
	}

	public event EventHandler<bool> ShowStateChanged;

	public virtual void Show(bool animate = true, Action completed = null)
	{
		if (Showing)
		{
			throw new InvalidOperationException();
		}
		CurrentShowState = ShowState.ShowAnimation;
		if (animate && anim != null)
		{
			anim.Play(SHOW_HASH);
			SimpleCoroutine.Create(base.gameObject).WaitUntil(delegate
			{
				AnimatorStateInfo currentAnimatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
				return currentAnimatorStateInfo.shortNameHash == SHOW_HASH && currentAnimatorStateInfo.normalizedTime >= 0.99f;
			}, delegate
			{
				CurrentShowState = ShowState.Show;
				OnShowCompleted();
				completed?.Invoke();
			});
		}
		else
		{
			CurrentShowState = ShowState.Show;
			OnShowCompleted();
			completed?.Invoke();
		}
	}

	protected virtual void OnShowCompleted()
	{
	}

	protected virtual void OnEnable()
	{
		dependObjects.ForEach(delegate(GameObject o)
		{
			o.SetActive(value: true);
		});
	}

	protected virtual void OnDisable()
	{
		dependObjects.ForEach(delegate(GameObject o)
		{
			if (o != null)
			{
				o.SetActive(value: false);
			}
		});
	}

	public virtual void Hide(bool animate = true, Action completed = null)
	{
		if (!Showing)
		{
			throw new InvalidOperationException();
		}
		CurrentShowState = ShowState.HideAnimation;
		if (animate && anim != null)
		{
			anim.Play(HIDE_HASH);
			SimpleCoroutine.Create(base.gameObject).WaitUntil(delegate
			{
				AnimatorStateInfo currentAnimatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
				return currentAnimatorStateInfo.shortNameHash == HIDE_HASH && currentAnimatorStateInfo.normalizedTime >= 0.99f;
			}, delegate
			{
				CurrentShowState = ShowState.Hide;
				OnHideCompleted();
				completed?.Invoke();
			});
		}
		else
		{
			CurrentShowState = ShowState.Hide;
			completed?.Invoke();
		}
	}

	protected virtual void OnHideCompleted()
	{
	}

	public void Exit()
	{
		Hide();
	}
}
