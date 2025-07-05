using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IColorData
{
    [Header("Character Settings")]
    [SerializeField] protected ColorDataConfig colorDataConfig;
    [SerializeField] protected EColorDataType colorDataType;
    [SerializeField] protected MeshRenderer mesh;

    [Header("Character State")]
    [SerializeField] private int brickCount = 0; // Số lượng gạch đã thu thập

    public int BrickCount
    {
        get => brickCount;
        set
        {
            int delta = value - brickCount;
            brickCount = value;

            BrickPlayerController(delta);
        }
    }

    [SerializeField] private Transform brickPlayerParent;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] Queue<GameObject> brickQueue = new Queue<GameObject>();

    private void Start()
    {
        if (mesh == null)
        {
            mesh = GetComponent<MeshRenderer>();
        }
        if (colorDataConfig == null)
        {
            Debug.LogWarning("ColorDataConfig is not set.");
        }
        else
        {
            ChangeMaterialColor(colorDataType, colorDataConfig);
        }
    }

    public void ChangeMaterialColor(EColorDataType colorDataType, ColorDataConfig colorDataConfig)
    {
        if (mesh == null || colorDataConfig == null)
        {
            Debug.LogWarning("MeshRenderer or ColorDataConfig is not set.");
            return;
        }

        Material material = colorDataConfig.GetMaterialColor((int)colorDataType);
        if (material != null)
        {
            mesh.material = material;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Xử lý 2 điều kiện: nếu là brick có tag "BrickPlatform" và có màu sắc trùng với màu sắc của chracter thì thực hiện hành động ẩn gạch và tăng số lượng brick của chracter

        if (!other.CompareTag(ConstTags.BRICK_PLATFORM) && !other.CompareTag(ConstTags.BRICK_BRIDGE)) return;

        BrickPlatform brickPlatform = other.GetComponent<BrickPlatform>();
        if (brickPlatform == null)
        {
            Debug.LogWarning("BrickPlatform component not found on the collided object.");
            return;
        }

        if (brickPlatform.GetColorDataType() != colorDataType || !brickPlatform.IsBrickVisible())
        {
            return;
        }

        if (other.CompareTag(ConstTags.BRICK_PLATFORM))
        {
            BrickCount++;
            brickPlatform.DisableBrick(); // Ẩn gạch
        }
        if (other.CompareTag(ConstTags.BRICK_BRIDGE))
        {
            BrickCount--;
            Debug.Log($"Brick count decreased: {BrickCount}");
        }
    }


    private void BrickPlayerController(int value)
    {


        if (value > 0)
        {
            // Add bricks
            for (int i = 0; i < value; i++)
            {
                GameObject brick;

                // Check for disabled bricks in queue
                if (brickQueue.Count > 0)
                {
                    brick = brickQueue.Dequeue();
                    if (!brick.activeSelf)
                    {
                        brick.SetActive(true);
                        continue;
                    }
                    // If brick is active, put it back and create new one
                    brickQueue.Enqueue(brick);
                }

                // Create new brick if none available
                var brickPos = brickPrefab.transform.position + new Vector3(0, 0.25f, 0) * brickQueue.Count;
                brick = Instantiate(brickPrefab, brickPos, Quaternion.identity, brickPlayerParent);
                brick.SetActive(true);
                brickQueue.Enqueue(brick);
            }
        }

        else if (value < 0)
        {
            // Remove bricks
            int bricksToRemove = Mathf.Abs(value);
            int removedCount = 0;

            // Create temporary queue to preserve active bricks
            Queue<GameObject> tempQueue = new Queue<GameObject>();

            while (brickQueue.Count > 0 && removedCount < bricksToRemove)
            {
                GameObject brick = brickQueue.Dequeue();

                if (brick.activeSelf)
                {
                    brick.SetActive(false);
                    removedCount++;
                }

                tempQueue.Enqueue(brick);
            }

            // Restore remaining bricks to queue
            while (tempQueue.Count > 0)
            {
                brickQueue.Enqueue(tempQueue.Dequeue());
            }
        }
    }

}
