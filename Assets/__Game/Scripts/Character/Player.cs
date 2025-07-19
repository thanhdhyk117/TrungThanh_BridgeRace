using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (GameManager.Ins.IsState(EGameState.GamePlay))
        {
            if (Input.GetMouseButton(0))
            {

                Vector3 nextPoint = JoystickControl.direct * speed * Time.deltaTime + TF.position;

                if (CanMove(nextPoint))
                {
                    TF.position = CheckGround(nextPoint);
                }

                if (JoystickControl.direct != Vector3.zero)
                {
                    playerSkin.forward = JoystickControl.direct;
                }

                ChangeAnimation(Consts.ANIM_RUN);
            }

            if (Input.GetMouseButtonUp(0))
            {
                ChangeAnimation(Consts.ANIM_IDLE);
            }
        }
    }
}
