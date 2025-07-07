using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BrickPlatform : MonoBehaviour, IColorData
{
    private MeshRenderer _meshRenderer;
    private EColorDataType _currentColorData;
    
    [SerializeField] private float timeToReset = 2;

    private void Start()
    {
        if (_meshRenderer == null) _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterialColor(EColorDataType colorDataType, ColorDataConfig colorDataConfig)
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
        _currentColorData = colorDataType; // Lưu màu sắc hiện tại
    }

    private IEnumerator ResetBrick()
    {
        yield return new WaitForSeconds(timeToReset); // Thời gian chờ trước khi gạch xuất hiện lại
        _meshRenderer.enabled = true; // Tắt hiển thị gạch
    }

    public void DisableBrick()
    {
        _meshRenderer.enabled = false; // Tắt hiển thị gạch
        StartCoroutine(ResetBrick()); // Bắt đầu coroutine để kích hoạt lại gạch sau một thời gian
    }

    public EColorDataType GetColorDataType()
    {
        return _currentColorData;
    }

    public bool IsBrickVisible()
    {
        return _meshRenderer.enabled; // Trả về trạng thái hiển thị của gạch
    }
    
    
}
