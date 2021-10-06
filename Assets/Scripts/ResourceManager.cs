using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] private List<TextAsset> _modeLvlAssets = new List<TextAsset>();

    private readonly Dictionary<GameMode, List<Level>> _modeAndLevels = new Dictionary<GameMode, List<Level>>();


    public static int Coins
    {
        get => PrefManager.GetInt("Coins");
        set { PrefManager.SetInt("Coins", value); }
    }

    protected override void OnInit()
    {
        base.OnInit();
        InitLevels();
    }

    private void InitLevels()
    {
        for (var i = 0; i < _modeLvlAssets.Count; i++)
        {
            _modeAndLevels.Add((GameMode)i, JsonUtility.FromJson<LevelGroup>(_modeLvlAssets[i].text).ToList());
        }
    }

    public static IEnumerable<Level> GetLevels(GameMode mode)
    {
        return Instance._modeAndLevels[mode];
    }

    public static Level GetLevel(GameMode mode, int no)
    {
        if (no >= Instance._modeAndLevels[mode].Count) return default;
        return Instance._modeAndLevels[mode][no - 1];
    }

    public static bool IsLevelLocked(GameMode mode, int no)
    {
        var completedLevel = GetCompletedLevel(mode);
        return no > completedLevel + 1;
    }

    public static int GetCompletedLevel(GameMode mode)
    {
        return PrefManager.GetInt($"{mode}_Level_Complete");
    }

    public static void CompleteLevel(GameMode mode, int lvl)
    {
        if (GetLevel(mode).no <= lvl) PrefManager.SetInt($"{mode}_Level_Complete", lvl);
    }

    public static bool HasLevel(GameMode mode, int lvl)
    {
        return GetLevels(mode).Count() >= lvl;
    }

    public static Level GetLevel(GameMode mode)
    {
        return GetLevel(mode, PrefManager.GetInt($"{mode}_Level_Complete") + 1);
    }
}