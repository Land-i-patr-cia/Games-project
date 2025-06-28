using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rb;
    
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello World");
        rb.useGravity = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(0,0,-forwardForce * Time.deltaTime);
        }
    }
}
