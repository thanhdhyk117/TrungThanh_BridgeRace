using UnityEngine;

public class CharacterController : MonoBehaviour, IColorData
{
    [SerializeField] protected ColorDataConfig colorDataConfig;
    [SerializeField] protected ColorDataType colorDataType;
    [SerializeField] protected MeshRenderer mesh;

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

    public void ChangeMaterialColor(ColorDataType colorDataType, ColorDataConfig colorDataConfig)
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
}
