using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    private void Start()
    {
        InitScript();
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        Camera.main.transform.position = transform.position + new Vector3(0, 12.5f, -20f);
    }

    private void InitScript()
    {
        if (variableJoystick == null)
        {
            variableJoystick = FindObjectOfType<VariableJoystick>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        variableJoystick.SetMode(JoystickType.Dynamic);
        variableJoystick.AxisOptions = AxisOptions.Both;
    }
}
