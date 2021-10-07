using Game;
using MyGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : ShowHidable
{
    [SerializeField] private TextMeshProUGUI _lvlTxt;
    public GameObject exitpanel;

    private void Start()
    {
        _lvlTxt.text = $"LEVEL {LevelManager.Instance.Level.no}";
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
       
            ResourceManager.CompleteLevel(LevelManager.Instance.GameMode, LevelManager.Instance.Level.no);
            UIManager.Instance.LoadNextLevel();
        
        // }
    }

    public void OnClickMenu()
    {
        /*SharedUIManager.PopUpPanel.ShowAsConfirmation("Exit?", "Are you sure want to exit the game?",
            delegate(bool success)
            {
                if (success)
                {
                    GameManager.LoadScene("MainMenu");
                }
            });*/
        exitpanel.SetActive(true);
    }

    public void no()
    {
        exitpanel.SetActive(false);
    }

    public void Exit()
    {
        GameManager.LoadScene("MainMenu");
    }

    private void Update()
    {
    }
}