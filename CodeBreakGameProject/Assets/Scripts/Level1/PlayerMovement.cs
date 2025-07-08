using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private Rigidbody rb;
    private Animator animator;
    private bool isMoving;
    public static bool IsPlayerMoving { get; private set; }

    void Start()
    {
        Debug.Log("PlayerController Start for " + gameObject.name);
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if (rb == null) Debug.LogError("Rigidbody missing on Player");
        if (animator == null) Debug.LogError("Animator missing on Player");
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rb.useGravity = false;
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S
        Vector3 movement = new Vector3(moveX, 0f, moveZ).normalized;

        if (movement.magnitude > 0)
        {
            Debug.Log($"Player input: moveX={moveX}, moveZ={moveZ}");
        }

        isMoving = movement.magnitude > 0;
        IsPlayerMoving = isMoving;
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
        if (rb != null)
        {
            rb.velocity = movement * moveSpeed;
        }
        else
        {
            Debug.LogError("Cannot move: Rigidbody is null");
        }
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RogueComponent"))
        {
            Debug.Log("Player hit RogueComponent, resetting to start");
            transform.position = new Vector3(0, 1, 0);
            if (rb != null) rb.velocity = Vector3.zero;
        }
    }
}