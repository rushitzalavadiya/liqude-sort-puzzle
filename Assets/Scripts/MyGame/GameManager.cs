using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame
{
	public class GameManager : Singleton<GameManager>
	{
		public static int TOTAL_GAME_COUNT
		{
			get
			{
				
				return PrefManager.GetInt("TOTAL_GAME_COUNT");
			}
			set
			{
				PrefManager.SetInt("TOTAL_GAME_COUNT", value);
			}
		}

		public static LoadGameData LoadGameData
		{
			get;
			set;
		}

		protected override void OnInit()
		{
			base.OnInit();
			Application.targetFrameRate = 60;
		}

		public static void LoadScene(string sceneName, bool showLoading = true, float loadingScreenSpeed = 5f)
		{
			LoadingPanel loadingPanel = SharedUIManager.LoadingPanel;
			if (showLoading && loadingPanel != null)
			{
				loadingPanel.Speed = loadingScreenSpeed;
				loadingPanel.Show(animate: true, delegate
				{
					SceneManager.LoadScene(sceneName);
					loadingPanel.Hide();
				});
			}
			else
			{
				SceneManager.LoadScene(sceneName);
			}
		}

		public static void LoadGame(LoadGameData data, bool showLoading = true, float loadingScreenSpeed = 1f)
		{
			LoadGameData = data;
			LoadScene("Main", showLoading, loadingScreenSpeed);
		}
	}
}
