using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Level level;

    private Level currentLevel => level;

    public Vector3 FinishPoint => currentLevel.finishPoint.position;

    public List<Bot> bots = new List<Bot>();

    [SerializeField] private Player player;
    [SerializeField] private Vector3[] startPoints;
    [SerializeField] private EColorDataType[] colorDatas;

    public int CharacterAmount => startPoints.Length;

    [SerializeField] private Button startButton;

    private void Start()
    {
        OnInit();

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartgame);
        }
        else
        {
            Debug.LogWarning("Start button not assigned in LevelManager!");
        }
    }

    private void OnInit()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned in LevelManager!");
            return;
        }

        player.OnInit();
        for (int i = 0; i < CharacterAmount; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot);
            bot.TF.position = startPoints[i];
            bot.ChangeColor(colorDatas[i]);
            bot.OnInit();
            bots.Add(bot);
        }
    }

    public void OnStartgame()
    {
        if (level == null)
        {
            Debug.LogError("Level not initialized in LevelManager!");
            return;
        }
        GameManager.Ins.ChangeState(EGameState.GamePlay);
        level.OnInit();
        for (int i = 0; i < bots.Count; i++)
        {
            //bots[i].ChangeAnimation(Consts.ANIM_RUN);
            //bots[i].SetDestination(level.endPoint.position);
            bots[i].ChangeState(new PatrolState());
        }
    }



    [ContextMenu("Load Component")]
    public void LoadComponent()
    {
        level = FindObjectOfType<Level>();
        player = FindObjectOfType<Player>();
        if (level == null)
        {
            Debug.LogError("Level not found in the scene!");
            return;
        }
    }
}