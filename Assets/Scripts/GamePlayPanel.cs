using Game;
using MyGame;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : ShowHidable
{
    [SerializeField] private Text _lvlTxt;

    private void Start()
    {
        _lvlTxt.text = $"Level {LevelManager.Instance.Level.no}";
    }

    public void OnClickUndo()
    {
        LevelManager.Instance.OnClickUndo();
    }

    public void OnClickRestart()
    {
        LoadGameData data = default(LoadGameData);
        data.Level = LevelManager.Instance.Level;
        data.GameMode = LevelManager.Instance.GameMode;
        GameManager.LoadGame(data, showLoading: false);
    }

    public void OnClickSkip()
    {
        // if (!AdsManager.IsVideoAvailable())
        // {
        // 	SharedUIManager.PopUpPanel.ShowAsInfo("Connection?", "Sorry no video ads available.Check your internet connection!");
        // }
        // else
        // {
        SharedUIManager.PopUpPanel.ShowAsConfirmation("Skip", "Watch Video to skip this level", delegate(bool success)
        {
            ResourceManager.CompleteLevel(LevelManager.Instance.GameMode, LevelManager.Instance.Level.no);
            UIManager.Instance.LoadNextLevel();
        });
        // }
    }

    public void OnClickMenu()
    {
        SharedUIManager.PopUpPanel.ShowAsConfirmation("Exit?", "Are you sure want to exit the game?",
            delegate(bool success)
            {
                if (success)
                {
                    GameManager.LoadScene("MainMenu");
                }
            });
    }

    private void Update()
    {
    }
}