using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float angle;
    
    [SerializeField] private Vector3 offset;
    
    private void Start()
    {
        InitScript();
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90;
        
        transform.eulerAngles = new Vector3(0, -angle, 0);
        
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        Camera.main.transform.position = transform.position + offset;
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

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }
}
