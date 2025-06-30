using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float smoothSpeed = 0.125f; // Adjust for smoother or faster follow
    public Vector3 offset; // Offset from the player

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is not assigned for the camera.");
            return;
        }

        // Calculate desired position
        Vector3 desiredPosition = player.position + player.rotation * offset;

        // Smoothly interpolate to desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        // Optionally, rotate the camera to look in the same direction as the player
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, smoothSpeed);
    }
}
