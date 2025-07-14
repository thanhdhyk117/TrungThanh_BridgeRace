using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create", menuName = "ScriptableObjects/ColorDataConfig", order = 1)]
public class ColorDataConfig : ScriptableObject
{
    public List<Material> materials;

    public Material GetMaterialColor(int index) => materials[index];
}

public enum EColorDataType
{
    None = 0,
    Red = 1,
    Green = 2,
    Blue = 3,
    Yellow = 4
}