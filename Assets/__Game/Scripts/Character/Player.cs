using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed;
    [SerializeField] private VariableJoystick joystick;

    private void Start()
    {
        joystick.SetMode(JoystickType.Dynamic);
        joystick.AxisOptions = AxisOptions.Both;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = joystick.Horizontal * Vector3.right + joystick.Vertical * Vector3.forward;
            Vector3 nextPoint = direction * speed * Time.deltaTime + TF.position;

            if (CanMove(nextPoint))
            {
                TF.position = nextPoint;
            }

            if (direction != Vector3.zero)
            {
                playerSkin.forward = direction;
            }
            
            ChangeAnimation(Consts.ANIM_RUN);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ChangeAnimation(Consts.ANIM_IDLE);
        }
    }
}
