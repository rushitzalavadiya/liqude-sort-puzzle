namespace MainMenu
{
	public class GameModePanel : ShowHidable
	{
		public void OnClickButton(int mode)
		{
			LevelsPanel levelsPanel = UIManager.Instance.LevelsPanel;
			levelsPanel.GameMode = (GameMode)mode;
			levelsPanel.Show();
		}
	}
}
