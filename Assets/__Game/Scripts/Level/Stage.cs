using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform brickPointParent;
    public Transform[] brickPoints;
    public List<Vector3> emptyPoint = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    [SerializeField] private Brick brickPrefab;

    [SerializeField] private int characterAmount = 4;
    
    public void OnInit()
    {
        for (var i = 0; i < brickPoints.Length; i++)
        {
            emptyPoint.Add(brickPoints[i].position);
        }
    }

    public void InitColor(EColorDataType colorType)
    {
        int amount = brickPoints.Length / characterAmount;
        if (brickPoints.Length % amount != 0)
        {
            amount++;
        }

        for (var i = 0; i < amount; i++)
        {
            NewBrick(colorType);
        }
    }

    public void NewBrick(EColorDataType colorType)
    {
        if (emptyPoint.Count <= 0) return;

        var random = Random.Range(0, emptyPoint.Count);
        Brick brick = SimplePool.Spawn<Brick>(brickPrefab, emptyPoint[random], Quaternion.identity);
        brick.stage = this;
        brick.ChangeColor(colorType);
        emptyPoint.RemoveAt(random);
        bricks.Add(brick);
    }

    public void RemoveBrick(Brick brick)
    {
        emptyPoint.Add(brick.TF.position);
        bricks.Remove(brick);
    }

    internal Brick SeekBrickPoint(EColorDataType colorType)
    {
        Brick brick = null;

        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                brick = bricks[i];
                break;
            }
        }

        return brick;
    }

    [ContextMenu("Load All Points")]
    private void LoadPoint()
    {
        if (brickPoints.Length == 0)
        {
            brickPoints = brickPointParent.GetComponentsInChildren<Transform>();
        }
    }
}
