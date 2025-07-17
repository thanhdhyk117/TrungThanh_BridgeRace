using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStageArea : MonoBehaviour
{
    public Stage stage;
    private List<EColorDataType> colorTypeList = new List<EColorDataType>();

    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);

        if (character != null && !colorTypeList.Contains(character.colorType))
        {
            character.ChangeColor(character.colorType);
            colorTypeList.Add(character.colorType);
            character.stage = stage;
            stage.InitColor(character.colorType);
        }
    }
}
