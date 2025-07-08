using UnityEngine;

public class RogueComponent : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 startPos;
    public float minX = -14.9f, maxX = 14.9f, minZ = -14.9f, maxZ = 14.9f; // Default for Level 2
    private Rigidbody playerRb;

    void Start()
    {
        Debug.Log("RogueComponent Start for " + gameObject.name);
        startPos = transform.position;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
            if (playerRb == null) Debug.LogError("Player Rigidbody not found");
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found");
        }
    }

    void Update()
    {
        bool isPlayerMoving = playerRb != null && playerRb.linearVelocity.magnitude > 0.1f;
        Debug.Log($"RogueComponent: isPlayerMoving={isPlayerMoving}, Player velocity={playerRb?.linearVelocity}");

        if (isPlayerMoving)
        {
            Vector3 movement = Vector3.forward * moveSpeed * Time.deltaTime;
            Vector3 newPos = transform.position + movement;
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);
            transform.position = newPos;
        }
    }
}