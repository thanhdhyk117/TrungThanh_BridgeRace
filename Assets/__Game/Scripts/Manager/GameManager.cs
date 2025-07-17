using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    MainMenu,
    GamePlay,
    Pause
}
public class GameManager : Singleton<GameManager>
{
    private EGameState gameState;

    private void Start()
    {
        ChangeState(EGameState.MainMenu);
    }

    public void ChangeState(EGameState gameState)
    {
        this.gameState = gameState; 
    }
    
    public bool IsState(EGameState gameState)
    {
        return this.gameState == gameState;
    }
}
