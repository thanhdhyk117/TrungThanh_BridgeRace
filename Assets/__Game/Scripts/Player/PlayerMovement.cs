using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float angle;

    [SerializeField] private Vector3 offset;

    [Header("Raycast Settings")]
    [SerializeField] private GameObject raycastObject;
    [SerializeField] private BoxCollider targetRaycast;
    [SerializeField] private LayerMask layerMask;

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

        SetUpRaycast();
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

    private void SetUpRaycast()
    {
        if (raycastObject == null || targetRaycast == null)
        {
            Debug.LogWarning("Raycast object or target raycast is not set.");
            return;
        }

        Physics.Raycast(raycastObject.transform.position, Vector3.down, out RaycastHit hit, 2f, layerMask);

        bool hasHit = hit.collider != null && hit.collider == targetRaycast;

        Debug.Log($"Raycast hit: {hit.collider.name}, Position + {hit.point}, Normal: {hit.normal}");

        Debug.DrawRay(raycastObject.transform.position, Vector3.down * 2, hasHit ? Color.red : Color.green, 0.5f);
    }
}
