using UnityEngine;

public class BrickPlatform : MonoBehaviour, IColorData
{
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        if(_meshRenderer == null) _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterialColor(ColorDataType colorDataType, ColorDataConfig colorDataConfig)
    {
        if (_meshRenderer == null)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            if (_meshRenderer == null)
            {
                Debug.LogError("MeshRenderer component not found on BrickPlatform.");
                return;
            }
        }
        _meshRenderer.material = colorDataConfig.GetMaterialColor((int)colorDataType);
    }
}
