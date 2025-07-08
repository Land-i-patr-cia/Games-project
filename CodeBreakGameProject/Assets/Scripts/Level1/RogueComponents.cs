using UnityEngine;

public class RogueComponent : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 startPos;
    private float minX = -9.9f, maxX = 9.9f, minZ = -9.9f, maxZ = 9.9f; // Level 1 wall bounds
    private Animator animator;

    void Start()
    {
        Debug.Log("RogueComponent Start for " + gameObject.name);
        startPos = transform.position;
        animator = GetComponent<Animator>();
        if (animator != null) animator.SetBool("isMoving", false);
        else Debug.LogError("Animator missing on RogueComponent");
    }

    void Update()
    {
        if (PlayerController.IsPlayerMoving)
        {
            Vector3 movement = Vector3.forward * moveSpeed * Time.deltaTime;
            Vector3 newPos = transform.position + movement;
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);
            transform.position = newPos;

            if (animator != null) animator.SetBool("isMoving", true);
        }
        else
        {
            if (animator != null) animator.SetBool("isMoving", false);
        }
    }
}