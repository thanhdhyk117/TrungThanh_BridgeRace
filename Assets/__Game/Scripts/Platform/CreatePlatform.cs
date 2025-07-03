using System.Collections.Generic;
using UnityEngine;
public class CreatePlatform : MonoBehaviour
{
    [SerializeField] private GameObject bridgePrefab;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private List<BrickPlatform> bricks = new List<BrickPlatform>();
    [SerializeField] private ColorDataConfig colorDataConfig;
    [SerializeField] private ColorDataType colorDataType = ColorDataType.None;

    [SerializeField] private int mapWidth = 10;

    [SerializeField] private int numberOfBricks = 4;

    [SerializeField] private List<ColorDataType> colorList = new List<ColorDataType>();

    private void Start()
    {
        colorList = MakeListColorMaterial();

    }

    private List<ColorDataType> MakeListColorMaterial()
    {
        int totalBrickCount = mapWidth * mapWidth;
        int numberOfBricksPerColor = totalBrickCount / numberOfBricks; // Exclude None
        int currentcolorIndex = 1;
        List<ColorDataType> colorList = new List<ColorDataType>();

        for (var i = 0; i < totalBrickCount; i++)
        {
            if (i + 1 % numberOfBricksPerColor == 0)
            {
                currentcolorIndex++;
            }

            colorList.Add((ColorDataType)currentcolorIndex);
        }

        return colorList;
    }


    private void MakeMap()
    {

    }
}
