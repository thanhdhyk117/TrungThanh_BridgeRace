using System.Collections.Generic;
using UnityEngine;

public class CreatePlatform : MonoBehaviour
{

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPointParent;

    [SerializeField] private int mapWidth = 10;
    [SerializeField] private int mapHeight = 10;

    
    [ContextMenu("Make Map")]
    private void MakeMap()
    {
        for (var i = 0; i < mapWidth; i++)
        {
            for (var j = 0; j < mapHeight; j++)
            {
                var x = spawnPoint.localScale.x;
                var z = spawnPoint.localScale.z;
                Vector3 spawnPosition = spawnPoint.position + new Vector3(i * x * 1.5f, 0, j * z * 2f);
                Transform newObj = Instantiate(spawnPoint, spawnPointParent);
                
                newObj.localPosition = spawnPosition;
                
            }
        }
    }

    [ContextMenu("Destroy Map")]
    private void DestroyMap()
    {
        for (int i = 0; i < spawnPointParent.childCount; i++)
        {
            Destroy(spawnPointParent.GetChild(i).gameObject);
        }
    }
}
