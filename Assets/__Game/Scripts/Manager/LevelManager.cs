using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private Level level;
    
    private Level currentLevel => level;
    
    public Vector3 FinishPoint => currentLevel.finishPoint.position;
}