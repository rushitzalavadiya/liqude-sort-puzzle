using MyGame;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public enum State
	{
		None,
		Playing,
		Over
	}

	public struct MoveData
	{
		public Holder FromHolder
		{
			get;
			set;
		}

		public Holder ToHolder
		{
			get;
			set;
		}

		public Liquid Liquid
		{
			get;
			set;
		}
	}

	[SerializeField]
	private float _minXDistanceBetweenHolders;

	[SerializeField]
	private Camera _camera;

	[SerializeField]
	private Holder _holderPrefab;

	[SerializeField]
	private AudioClip _winClip;

	private readonly List<Holder> _holders = new List<Holder>();

	private readonly Stack<MoveData> _undoStack = new Stack<MoveData>();

	public static LevelManager Instance
	{
		get;
		private set;
	}

	public GameMode GameMode
	{
		get;
		private set;
	}

	public Level Level
	{
		get;
		private set;
	}

	public State CurrentState
	{
		get;
		private set;
	}

	public bool HaveUndo => _undoStack.Count > 0;

	public bool IsTransfer
	{
		get;
		set;
	}

	public static event Action LevelCompleted;

	private void Awake()
	{
		Instance = this;
		LoadGameData loadGameData = GameManager.LoadGameData;
		GameMode = loadGameData.GameMode;
		Level = loadGameData.Level;
		LoadLevel();
		CurrentState = State.Playing;
	}

	private void LoadLevel()
	{
		float expectWidth;
		List<Vector2> list = PositionsForHolders(Level.map.Count, out expectWidth).ToList();
		_camera.orthographicSize = 0.5f * expectWidth * (float)Screen.height / (float)Screen.width;
		List<IEnumerable<LiquidData>> liquidDataMap = Level.LiquidDataMap;
		for (int i = 0; i < liquidDataMap.Count; i++)
		{
			IEnumerable<LiquidData> liquidDatas = liquidDataMap[i];
			Holder holder = UnityEngine.Object.Instantiate(_holderPrefab, list[i], Quaternion.identity);
			holder.Init(liquidDatas);
			_holders.Add(holder);
		}
	}

	public void OnClickUndo()
	{
		if (CurrentState == State.Playing && _undoStack.Count > 0)
		{
			_undoStack.Pop();
		}
	}

	private void Update()
	{
		if (CurrentState != State.Playing || !Input.GetMouseButtonDown(0))
		{
			return;
		}
		Collider2D collider2D = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition));
		if (collider2D != null)
		{
			Holder component = collider2D.GetComponent<Holder>();
			if (component != null)
			{
				OnClickHolder(component);
			}
		}
	}

	private void OnClickHolder(Holder holder)
	{
		// if (IsTransfer)
		// {
		// 	return;
		// }
		Holder holder2 = _holders.FirstOrDefault((Holder h) => h.IsPending);
		if (holder2 != null && holder2 != holder)
		{
			if (holder.TopLiquid == null || (holder2.TopLiquid.GroupId == holder.TopLiquid.GroupId && !holder.IsFull))
			{
				IsTransfer = true;
				StartCoroutine(SimpleCoroutine.CoroutineEnumerator(holder2.MoveAndTransferLiquid(holder, CheckAndGameOver), delegate
				{
					IsTransfer = false;
				}));
			}
			else
			{
				holder2.ClearPending();
				holder.StartPending();
			}
		}
		else if (holder.Liquids.Any())
		{
			if (!holder.IsPending)
			{
				holder.StartPending();
			}
			else
			{
				holder.ClearPending();
			}
		}
	}

	private void CheckAndGameOver()
	{
		if (_holders.All(delegate(Holder holder)
		{
			List<Liquid> list = holder.Liquids.ToList();
			return (list.Count == 0) || (list.Count == 1);
		}) && (from holder in _holders
			where holder.Liquids.Any()
			group holder by holder.Liquids.First().GroupId).All((IGrouping<int, Holder> holders) => holders.Count() == 1))
		{
			OverTheGame();
		}
	}

	private void OverTheGame()
	{
		if (CurrentState == State.Playing)
		{
			PlayClipIfCan(_winClip);
			CurrentState = State.Over;
			ResourceManager.CompleteLevel(GameMode, Level.no);
			LevelManager.LevelCompleted?.Invoke();
		}
	}

	private void PlayClipIfCan(AudioClip clip, float volume = 0.35f)
	{
		if (AudioManager.IsSoundEnable && !(clip == null))
		{
			AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
		}
	}

	public IEnumerable<Vector2> PositionsForHolders(int count, out float expectWidth)
	{
		expectWidth = 4f * _minXDistanceBetweenHolders;
		if (count <= 6)
		{
			Vector3 minPoint = base.transform.position - (float)(count - 1) / 2f * _minXDistanceBetweenHolders * Vector3.right - Vector3.up * 1f;
			expectWidth = Mathf.Max((float)count * _minXDistanceBetweenHolders, expectWidth);
			return from i in Enumerable.Range(0, count)
				select (Vector2)minPoint + (float)i * _minXDistanceBetweenHolders * Vector2.right;
		}
		float num = (float)Screen.width / (float)Screen.height;
		int num2 = Mathf.CeilToInt((float)count / 2f);
		if ((float)(num2 + 1) * _minXDistanceBetweenHolders > expectWidth)
		{
			expectWidth = (float)(num2 + 1) * _minXDistanceBetweenHolders;
		}
		float d = expectWidth / num;
		List<Vector2> list = new List<Vector2>();
		Vector3 topRowMinPoint = base.transform.position + Vector3.up * d / 6f - (float)(num2 - 1) / 2f * _minXDistanceBetweenHolders * Vector3.right - Vector3.up * 1f;
		list.AddRange(from i in Enumerable.Range(0, num2)
			select (Vector2)topRowMinPoint + (float)i * _minXDistanceBetweenHolders * Vector2.right);
		Vector3 lowRowMinPoint = base.transform.position - Vector3.up * d / 6f - (float)(count - num2 - 1) / 2f * _minXDistanceBetweenHolders * Vector3.right - Vector3.up * 1f;
		list.AddRange(from i in Enumerable.Range(0, count - num2)
			select (Vector2)lowRowMinPoint + (float)i * _minXDistanceBetweenHolders * Vector2.right);
		return list;
	}
}
