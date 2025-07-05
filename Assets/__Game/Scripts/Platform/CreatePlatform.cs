using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatePlatform : MonoBehaviour
{
    [SerializeField] private BrickPlatform bridgePrefab;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private List<BrickPlatform> bricks = new List<BrickPlatform>();
    [SerializeField] private ColorDataConfig colorDataConfig;

    [SerializeField] private int mapWidth = 10;

    [SerializeField] private int numberOfBricks = 4;

    [SerializeField] private List<ColorDataType> colorList = new List<ColorDataType>();
    
    private void Start()
    {
        ShuffleColorList();
        Invoke(nameof(MakeMap), 1);
    }

    private List<ColorDataType> MakeListColorMaterial()
    {
        var totalBrickCount = mapWidth * mapWidth;
        var numberOfBricksPerColor = totalBrickCount / numberOfBricks; // Exclude None
        var currentColorIndex = 1;
        var colorDataList = new List<ColorDataType>();

        // for (var i = 0; i < totalBrickCount; i++)
        // {
        //     if ((i + 1) % numberOfBricksPerColor == 0)
        //     {
        //         currentColorIndex++;
        //     }
        //
        //     colorDataList.Add((ColorDataType)currentColorIndex);
        // }
        
        for (var i = 0; i < totalBrickCount; i++)
        {
            if (i > 0 && i % numberOfBricksPerColor == 0 && currentColorIndex < numberOfBricks)
            {
                currentColorIndex++;
            }
            
            if (currentColorIndex >= (int)System.Enum.GetValues(typeof(ColorDataType)).Length)
            {
                Debug.LogError($"Invalid ColorDataType index: {currentColorIndex}");
                currentColorIndex = 1; 
            }
            colorDataList.Add((ColorDataType)currentColorIndex);
        }

        return colorDataList;
    }

    private void MakeMap()
    {
        //Make map
        for (var i = 0; i < mapWidth; i++)
        {
            for(var j = 0; j < mapWidth; j++)
            {
                var parentTransform = transform.GetChild(0);
                var spawnPointPos = spawnPoint.position + new Vector3(i * 2, 0, j);
                var brick = Instantiate(bridgePrefab, spawnPointPos, Quaternion.identity);
                brick.transform.SetParent(parentTransform);
                brick.gameObject.SetActive(true);
                brick.ChangeMaterialColor(colorList[i * mapWidth + j], colorDataConfig);
                bricks.Add(brick);
            }
        }
    }
    
    private void ShuffleColorList()
    {
        colorList = MakeListColorMaterial();
        if (colorList.Count <= 1) return;

        for (int i = colorList.Count - 1; i >= 0; i--) // Fixed loop range
        {
            int j = UnityEngine.Random.Range(0, i + 1); // Use Unity's Random
            ColorDataType temp = colorList[i];
            colorList[i] = colorList[j];
            colorList[j] = temp;
        }
    }

}
