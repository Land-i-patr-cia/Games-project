using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float smoothSpeed = 0.125f; // Adjust for smoother or faster follow
    public Vector3 offset; // Offset from the player

    void LateUpdate()
    {
        if (player == null) return; // Handle cases where the player is not assigned

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}