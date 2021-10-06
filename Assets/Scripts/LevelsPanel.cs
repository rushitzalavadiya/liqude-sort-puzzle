using MyGame;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsPanel : ShowHidable
{
    [SerializeField] private LevelTileUI _levelTileUIPrefab;

    [SerializeField] private RectTransform _content;

    private readonly List<LevelTileUI> _tiles = new List<LevelTileUI>();

    public List<Level> list;

    private GameMode _gameMode;

    public GameMode GameMode
    {
        get { return _gameMode; }
        set
        {
            _gameMode = value;
            list = ResourceManager.GetLevels(value).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Level level = list[i];
                if (_tiles.Count <= i)
                {
                    LevelTileUI levelTileUI = Instantiate(_levelTileUIPrefab, _content);
                    levelTileUI.Clicked += LevelTileUIOnClicked;
                    _tiles.Add(levelTileUI);
                }

                _tiles[i].MViewModel = new LevelTileUI.ViewModel
                {
                    Level = level,
                    Locked = ResourceManager.IsLevelLocked(value, level.no),
                    Completed = (ResourceManager.GetCompletedLevel(value) >= level.no)
                };
            }
        }
    }

    private void LevelTileUIOnClicked(LevelTileUI tileUI)
    {
        if (tileUI.MViewModel.Locked) return;
        LoadGameData data = default(LoadGameData);
        data.Level = tileUI.MViewModel.Level;
        data.GameMode = GameMode;
        GameManager.LoadGame(data);
    }
}