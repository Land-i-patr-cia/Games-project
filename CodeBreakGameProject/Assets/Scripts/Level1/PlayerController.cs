using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 5f; // Set to 5 units/s
    public float rotationSpeed = 100f; // For smooth rotation

    void Start()
    {
        Debug.Log("PlayerController Start for " + gameObject.name);
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogError("Rigidbody missing on Player");
        if (rb != null)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; // Left/Right arrows
        movementY = movementVector.y; // Up/Down arrows
        Debug.Log($"Input: moveX={movementX}, moveY={movementY}");
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY).normalized * speed;
        if (rb != null)
        {
            rb.AddForce(movement, ForceMode.Force);
            Debug.Log($"Player velocity: {rb.linearVelocity}");
        }

        // Rotate player to face movement direction
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RogueComponent"))
        {
            Debug.Log("Player hit RogueComponent, resetting to Level 1");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        }
    }
}