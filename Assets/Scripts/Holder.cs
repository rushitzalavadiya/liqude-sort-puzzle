using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Holder : MonoBehaviour
{
	[SerializeField]
	private int _maxValue = 4;

	[SerializeField]
	private float _ballRadius;

	[SerializeField]
	private AudioClip _popClip;

	[SerializeField]
	private AudioClip _putClip;

	[SerializeField]
	private Liquid _liquidPrefab;

	[SerializeField]
	private Transform _content;

	[SerializeField]
	private Transform _leftSideDeliverPoint;

	[SerializeField]
	private Transform _rightSideDeliverPoint;

	[SerializeField]
	private Vector2 _transferNearOffset;

	[SerializeField]
	private SpriteRenderer _liquidLine;

	[SerializeField]
	private AudioSource _audio;

	[SerializeField]
	private AudioClip _liquidTransferClip;

	private readonly List<Liquid> _liquids = new List<Liquid>();

	private Coroutine _moveCoroutine;

	private bool _isFront;

	public bool IsFull => Mathf.RoundToInt(_liquids.Sum((Liquid l) => l.Value)) >= _maxValue;

	public Liquid TopLiquid => _liquids.LastOrDefault();

	public IEnumerable<Liquid> Liquids => _liquids;

	public int MAXValue => _maxValue;

	public float CurrentTotal => Liquids.Sum((Liquid l) => l.Value);

	public bool isThisHolderFull;

	public bool IsPending
	{
		get;
		private set;
	}

	public bool Initialized
	{
		get;
		private set;
	}

	public Vector2 PendingPoint
	{
		get;
		private set;
	}

	public Vector3 OriginalPoint
	{
		get;
		private set;
	}

	public bool IsFront
	{
		get
		{
			return _isFront;
		}
		set
		{
			_isFront = value;
			foreach (SpriteRenderer item in GetComponentsInChildren<SpriteRenderer>().Except(new SpriteRenderer[1]
			{
				_liquidLine
			}))
			{
				item.sortingLayerName = (value ? "Front" : "Default");
			}
		}
	}

	public void StartPending()
	{
		if (IsPending)
		{
			throw new InvalidOperationException();
		}
		IsPending = true;
		IsFront = true;
		MoveTo(PendingPoint, 5f);
		PlayClipIfCan(_popClip);
	}

	public void ClearPending()
	{
		IsPending = false;
		IsFront = false;
		MoveTo(OriginalPoint, 5f);
		PlayClipIfCan(_putClip);
	}

	private IEnumerator MoveNearToHolderForTransfer(Holder holder)
	{
		Vector3 vector = holder.transform.TransformPoint((base.transform.position.x > holder.transform.position.x) ? _transferNearOffset.WithX(Mathf.Abs(_transferNearOffset.x)) : _transferNearOffset.WithX(0f - Mathf.Abs(_transferNearOffset.x)));
		float speedForDistance = GetSpeedForDistance((base.transform.position - vector).magnitude);
		StopMoveIfAlready();
		yield return MoveToEnumerator(vector, Mathf.Max(speedForDistance, 3f));
	}

	private float GetSpeedForDistance(float distance)
	{
		return 5f / distance;
	}

	private IEnumerator ReturnToOriginalPoint()
	{
		StopMoveIfAlready();
		float speedForDistance = GetSpeedForDistance((base.transform.position - OriginalPoint).magnitude);
		yield return MoveToEnumerator(OriginalPoint, Mathf.Max(speedForDistance, 3f));
	}

	private void StopMoveIfAlready()
	{
		if (_moveCoroutine != null)
		{
			StopCoroutine(_moveCoroutine);
		}
	}

	public IEnumerator MoveAndTransferLiquid(Holder holder, Action onLiquidTransferComplete = null)
	{
		IsPending = false;
		int deliverAbsAngle = 82;
		Vector3 deliverTopPosition = holder.transform.TransformPoint(5f * Vector3.up);
		if (!holder.IsFull && _liquids.Any() && (!holder.Liquids.Any() || holder.Liquids.Last().GroupId == Liquids.Last().GroupId))
		{
			yield return MoveNearToHolderForTransfer(holder);
			bool flag = holder.transform.position.x > base.transform.position.x;
			Transform sidePoint = flag ? _rightSideDeliverPoint : _leftSideDeliverPoint;
			int num = flag ? (-deliverAbsAngle) : deliverAbsAngle;
			Vector3 point = base.transform.position - sidePoint.position;
			Vector3 a = Quaternion.AngleAxis(num, Vector3.forward) * point;
			Vector3 targetHolderPoint = a + deliverTopPosition;
			Quaternion targetHolderRotation = Quaternion.AngleAxis(num, Vector3.forward);
			Vector3 startPoint = base.transform.position;
			Quaternion startRotation = base.transform.rotation;
			Liquid thisLiquid = _liquids.Last();
			yield return SimpleCoroutine.MoveTowardsEnumerator(0f, 1f, delegate(float n)
			{
				base.transform.position = Vector3.Lerp(startPoint, targetHolderPoint, n);
				base.transform.rotation = Quaternion.Lerp(startRotation, targetHolderRotation, n);
			}, null, 2f);
			float thisLiquidStartValue = thisLiquid.Value;
			float transferValue = Mathf.Min(thisLiquid.Value, (float)holder.MAXValue - holder.CurrentTotal);
			if (holder.Liquids.LastOrDefault() == null)
			{
				holder.AddLiquid(thisLiquid.GroupId);
			}
			Liquid targetLiquid = holder.Liquids.Last();
			float targetLiquidStartValue = targetLiquid.Value;
			_liquidLine.transform.position = sidePoint.position;
			_liquidLine.gameObject.SetActive(value: true);
			_liquidLine.transform.localScale = _liquidLine.transform.localScale.WithY(sidePoint.transform.position.y - holder.transform.position.y);
			_liquidLine.color = thisLiquid.Renderer.color;
			_liquidLine.transform.rotation = Quaternion.identity;
			_audio.clip = _liquidTransferClip;
			_audio.Play();
			_audio.volume = transferValue / 5f;
			yield return SimpleCoroutine.MoveTowardsEnumerator(0f, 1f, delegate(float n)
			{
				thisLiquid.Value = Mathf.Lerp(thisLiquidStartValue, thisLiquidStartValue - transferValue, n);
				targetLiquid.Value = Mathf.Lerp(targetLiquidStartValue, targetLiquidStartValue + transferValue, n);
			}, null, 2f);
			if (thisLiquid.Value <= 0.05f)
			{
				_liquids.Remove(thisLiquid);
				UnityEngine.Object.Destroy(thisLiquid.gameObject);
			}
			else
			{
				thisLiquid.Value = Mathf.RoundToInt(thisLiquid.Value);
			}
			_audio.Stop();
			_liquidLine.gameObject.SetActive(value: false);
			targetLiquid.Value = Mathf.RoundToInt(targetLiquid.Value);
			onLiquidTransferComplete?.Invoke();
			yield return SimpleCoroutine.MoveTowardsEnumerator(0f, 1f, delegate(float n)
			{
				base.transform.position = Vector3.Lerp(targetHolderPoint, startPoint, n);
				base.transform.rotation = Quaternion.Lerp(targetHolderRotation, startRotation, n);
			}, null, 2f);
			yield return ReturnToOriginalPoint();
			IsFront = false;
		}

	}

	public void AddLiquid(int groupId, float value = 0f)
	{
		Vector2 topPoint = GetTopPoint();
		Liquid liquid = UnityEngine.Object.Instantiate(_liquidPrefab, _content);
		liquid.IsBottomLiquid = !Liquids.Any();
		liquid.GroupId = groupId;
		liquid.transform.position = topPoint;
		liquid.Value = value;
		_liquids.Add(liquid);
	}

	public Vector2 GetTopPoint()
	{
		return base.transform.TransformPoint(Liquids.Sum((Liquid l) => l.Size) * Vector2.up);
	}

	private void PlayClipIfCan(AudioClip clip, float volume = 0.35f)
	{
		if (AudioManager.IsSoundEnable && !(clip == null))
		{
			AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
		}
	}

	public void Init(IEnumerable<LiquidData> liquidDatas)
	{
		List<LiquidData> list = liquidDatas.ToList();
		if (!Initialized)
		{
			list.ForEach(delegate(LiquidData l)
			{
				AddLiquid(l.groupId, l.value);
			});
			PendingPoint = base.transform.position + 0.5f * Vector3.up;
			OriginalPoint = base.transform.position;
			Initialized = true;
		}
	}

	public void MoveTo(Vector2 point, float speed = 1f, Action onFinished = null)
	{
		StopMoveIfAlready();
		_moveCoroutine = StartCoroutine(SimpleCoroutine.CoroutineEnumerator(MoveToEnumerator(point, speed), onFinished));
	}

	private IEnumerator MoveToEnumerator(Vector2 toPoint, float speed = 1f)
	{
		Vector3 startPoint = base.transform.position;
		yield return SimpleCoroutine.MoveTowardsEnumerator(0f, 1f, delegate(float n)
		{
			base.transform.position = Vector3.Lerp(startPoint, toPoint, n);
		}, null, speed);
	}
}
