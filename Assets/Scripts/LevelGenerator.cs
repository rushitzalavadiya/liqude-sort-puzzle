using UnityEngine;

public class LevelGenerator : ScriptableObject
{
	[SerializeField]
	private Vector2Int _targetSwapRange;

	[SerializeField]
	private Vector2Int _targetGroupRange;

	[SerializeField]
	private Vector2Int _extraHolderRange;

	[SerializeField]
	private int _targetLevelCount;

	public const string TARGET_SWAP_RANGE = "_targetSwapRange";

	public const string TARGET_GROUP_RANGE = "_targetGroupRange";

	public const string EXTRA_HOLDER_RANGE = "_extraHolderRange";

	public const string TARGET_LEVEL_COUNT = "_targetLevelCount";
}
