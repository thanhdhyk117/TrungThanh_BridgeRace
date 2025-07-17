using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : ColorObject
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stairLayer;
    
    private List<PlayerBrick> playerBricks = new List<PlayerBrick>();
    
    [SerializeField] private PlayerBrick playerBrickPrefab;
    [SerializeField] private Transform playerBrickSpawn;
    [SerializeField] private Vector3 playerBrickSpawnOffset;
    
    [SerializeField] protected Transform playerSkin;
    
    [SerializeField] private Animator animator;
    
    private string currentAnimation;
    
    public Stage stage;
    
    public int BrickCount => playerBricks.Count;

    public override void OnInit()
    {
        ClearBrick();
        ChangeAnimation("Idle");
    }

    #region BrickPlayer

    /// <summary>
    /// Kiem tra xem nguoi choi co dang o tren stage (ground) hay khong
    /// 
    /// </summary>

    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.4f;
        }

        return TF.position;
    }
    public bool CanMove(Vector3 nextPoint)
    {
        //check mau stair
        //k cung mau -> fill
        //het gach + k cung mau + huong di len

        bool isCanMove = true;
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stairLayer))
        {
            Stair stair = Cache.GetStair(hit.collider);

            if (stair.colorType != colorType && playerBricks.Count > 0)
            {
                stair.ChangeColor(colorType);
                RemoveBrick();
                stage.NewBrick(colorType);
            }

            if (stair.colorType != colorType && playerBricks.Count == 0 && playerSkin.forward.z > 0)
            {
                isCanMove = false;
            }
        }

        return isCanMove;
    }
    
    private void AddBrick()
    {
        PlayerBrick playerBrick = Instantiate(playerBrickPrefab, playerBrickSpawn);
        playerBrick.ChangeColor(colorType);
        playerBrick.TF.localPosition = playerBrickSpawnOffset * playerBricks.Count;
        playerBricks.Add(playerBrick);
    }

    private void RemoveBrick()
    {
        if (playerBricks.Count > 0)
        {
            int brickIndex = playerBricks.Count - 1;
            PlayerBrick playerBrick = playerBricks[brickIndex];
            playerBricks.RemoveAt(brickIndex);
            Destroy(playerBrick.gameObject);
        }
    }

    private void ClearBrick()
    {
        foreach (var playerBrick in playerBricks)
        {
            Destroy(playerBrick.gameObject);
        }
        playerBricks.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.TAG_BRICK))
        {
            Brick brick = Cache.GetBrick(other);
            if (brick.colorType == colorType)
            {
                brick.OnDespawn();
                AddBrick();
                Destroy(brick.gameObject);
            }
        }
    }

    #endregion

    
    /// <summary>
    /// chuyen anim
    /// </summary>
    /// <param name="animationName"></param>
    public void ChangeAnimation(string animationName)
    {
        if (currentAnimation != animationName)
        {
            animator.ResetTrigger(currentAnimation);
            currentAnimation = animationName;
            animator.SetTrigger(currentAnimation);
        }
    }
}
