using System.Collections;
using System.Collections.Generic;   
using System;
using UnityEngine;

public class Brick : ColorObject
{
    public Stage stage;

    public override void OnDespawn()
    {
        stage.RemoveBrick(this);
    }
}
