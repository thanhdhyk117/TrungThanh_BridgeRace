using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Level[] levels;
    private Level _currentLevel;
    private int _currentLevelIndex;
    public List<Bot> bots = new List<Bot>();

    [SerializeField] private Player player;
    [SerializeField] private Vector3[] startPoints;
    [SerializeField] private EColorDataType[] colorDatas;
    [SerializeField] private Vector3 playerStartPoint;
    public Vector3 FinishPoint => _currentLevel.finishPoint.position;
    public int CharacterAmount => startPoints.Length;

    private void Awake()
    {
        _currentLevelIndex = PlayerPrefs.GetInt("Level", 0);
    }

    private void Start()
    {
        LoadLevel(_currentLevelIndex);
        OnInit();
        UIManager.Ins.OpenUI<MianMenu>();
    }

    private void OnInit()
    {
        
        player.OnInit();
        for (int i = 0; i < CharacterAmount; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot);
            bot.TF.position = startPoints[i];
            bot.ChangeColor(colorDatas[i]);
            bot.OnInit();
            bots.Add(bot);
        }
        player.TF.position = playerStartPoint;
    }
    
    public void OnStartgame()
    {
        GameManager.Ins.ChangeState(EGameState.GamePlay);
        _currentLevel?.OnInit();
        for (var i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new PatrolState());
        }
    }

    public void OnFinshgame()
    {
        foreach (var bot in bots)
        {
            bot.ChangeState(null);
            bot.MoveStop();
        }
        
    }
    public void OnReset()
    {
        SimplePool.CollectAll();
        bots.Clear();
    }

    internal void OnRetry()
    {
        OnReset();
        LoadLevel(_currentLevelIndex);
        OnInit();
        UIManager.Ins.OpenUI<MianMenu>();
    }

    internal void OnNextLevel()
    {
        _currentLevelIndex++;
        PlayerPrefs.SetInt("Level", _currentLevelIndex);
        OnReset();
        LoadLevel(_currentLevelIndex);
        OnInit();
        UIManager.Ins.OpenUI<MianMenu>();
    }

    public void LoadLevel(int index)
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
        }

        if (index < levels.Length)
        {
            _currentLevelIndex = index;
            _currentLevel = Instantiate(levels[_currentLevelIndex]);
            _currentLevel.OnInit();
        }
    }
    
}