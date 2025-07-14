using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : GameUnit
{
    public EColorDataType colorType;
    
    [SerializeField] private ColorDataConfig colorData;
    [SerializeField] private Renderer renderer;

    public void ChangeColor(EColorDataType colorType)
    {
        this.colorType = colorType;
        renderer.material = colorData.GetMaterialColor((int)colorType);
    }
    
    
    public override void OnInit()
    {
        
    }

    public override void OnDespawn()
    {
        
    }
}
