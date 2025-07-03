using UnityEngine;

public class BrickPlatform : MonoBehaviour, IColorData
{
    private MeshRenderer meshRenderer;


    public void ChangeMaterialColor(ColorDataType colorDataType, ColorDataConfig colorDataConfig)
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                Debug.LogError("MeshRenderer component not found on BrickPlatform.");
                return;
            }
        }
        meshRenderer.material = colorDataConfig.GetMaterialColor((int)colorDataType);
    }
}
