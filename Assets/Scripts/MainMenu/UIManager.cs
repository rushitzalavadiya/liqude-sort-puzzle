using UnityEngine;

namespace MainMenu
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private LevelsPanel _levelsPanel;

		[SerializeField]
		private GameModePanel _gameModePanel;

		public static UIManager Instance
		{
			get;
			private set;
		}

		public GameModePanel GameModePanel => _gameModePanel;

		public LevelsPanel LevelsPanel => _levelsPanel;

		private void Awake()
		{
			Instance = this;
		}
	}
}
