using MyGame;
using System.Collections;
using UnityEngine;

namespace Game
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private LevelCompletePanel _levelCompletePanel;

		[SerializeField]
		private TutorialPanel _tutorialPanel;

		[SerializeField]
		private GameObject _winEffect;

		public static UIManager Instance
		{
			get;
			private set;
		}

		public static bool IsFirstTime
		{
			get
			{
				return PrefManager.GetBool("IsFirstTime", def: true);
			}
			set
			{
				PrefManager.SetBool("IsFirstTime", value);
			}
		}

		private void Awake()
		{
			Instance = this;
			if (IsFirstTime)
			{
				_tutorialPanel.Show();
				IsFirstTime = false;
			}
		}

		private void OnEnable()
		{
			LevelManager.LevelCompleted += LevelManagerOnLevelCompleted;
		}

		private void OnDisable()
		{
			LevelManager.LevelCompleted -= LevelManagerOnLevelCompleted;
		}

		private void LevelManagerOnLevelCompleted()
		{
			StartCoroutine(LevelCompletedEnumerator());
		}

		private IEnumerator LevelCompletedEnumerator()
		{
			yield return new WaitForSeconds(0.2f);
			Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f)).WithZ(0f);
			Object.Instantiate(_winEffect, position, Quaternion.identity);
			yield return new WaitForSeconds(0.5f);
			_levelCompletePanel.Show();
		}

		public void LoadNextLevel()
		{
			GameMode gameMode = LevelManager.Instance.GameMode;
			int no = LevelManager.Instance.Level.no;
			if (!ResourceManager.HasLevel(gameMode, no + 1))
			{
				SharedUIManager.PopUpPanel.ShowAsInfo("Congratulations!", "You have successfully Completed this Game Mode", delegate
				{
					GameManager.LoadScene("MainMenu");
				});
				return;
			}
			LoadGameData data = default(LoadGameData);
			data.Level = ResourceManager.GetLevel(gameMode, no + 1);
			data.GameMode = gameMode;
			GameManager.LoadGame(data);
		}
	}
}
