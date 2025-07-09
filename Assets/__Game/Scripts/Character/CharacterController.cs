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
            brickCount = Mathf.Max(0, value);

            BrickPlayerController();
        }
    }

    [SerializeField] private Transform brickPlayerParent;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private List<GameObject> listBricks = new List<GameObject>();
    [SerializeField] private Vector3 offsetBrickPosition;

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

        if (other.CompareTag(ConstTags.BRICK_PLATFORM))
        {
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
        }

        if (other.CompareTag(ConstTags.BRICK_BRIDGE))
        {
            BrickCount--;
            Debug.Log($"Brick count decreased: {BrickCount}");
        }

    }

    private void ClearBrickList()
    {
        foreach (GameObject brick in listBricks)
        {
            if (brick != null)
            {
                Destroy(brick);
            }
        }
        listBricks.Clear();
    }
    private void BrickPlayerController()
    {
        while (listBricks.Count < brickCount)
        {
            var newBrickPlayer = Instantiate(brickPrefab, brickPlayerParent);
            newBrickPlayer.SetActive(false);
            listBricks.Add(newBrickPlayer);
        }

        for (var i = 0; i < listBricks.Count; i++)
        {
            listBricks[i].SetActive(i < brickCount);
            listBricks[i].GetComponent<MeshRenderer>().material = colorDataConfig.GetMaterialColor((int)colorDataType);
            if (i < brickCount)
            {
                listBricks[i].transform.position = brickPlayerParent.transform.position + i * offsetBrickPosition;
            }
        }
    }
}
