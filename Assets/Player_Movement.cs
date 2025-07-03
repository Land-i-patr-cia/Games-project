using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    public float rotationSpeed = 100f;

    private float yaw; // Rotation around Y axis

    void Start()
    {
        Debug.Log("Player movement initialized.");
        rb.useGravity = true;

        yaw = transform.eulerAngles.y;
    }

    void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            rb.AddForce(transform.right * sidewaysForce * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-transform.right * sidewaysForce * Time.deltaTime);
        }
        if (Input.GetKey("w"))
        {
            rb.AddForce(transform.forward * forwardForce * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(-transform.forward * forwardForce * Time.deltaTime);
        }
    }

    void Update()
    {
        if (Input.GetKey("q"))
        {
            yaw -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            yaw += rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0, yaw, 0);
    }
}
