using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinArea : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      Character character = Cache.GetCharacter(other);
      if (character != null)
      {
         character.ChangeAnimation("Victory");
         character.TF.eulerAngles = Vector3.up * 180;
         character.OnInit();
      }
   }
}
