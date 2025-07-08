using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float minZ = -10f;
    public float maxZ = 10f;
    private bool movingForward = true;

    void Update()
    {
        float newZ = transform.position.z + (movingForward ? moveSpeed : -moveSpeed) * Time.deltaTime;
        if (newZ > maxZ)
        {
            newZ = maxZ;
            movingForward = false;
        }
        else if (newZ < minZ)
        {
            newZ = minZ;
            movingForward = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        // Make player stick to platform
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(2.5f, 0.1f, 2.5f));
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                col.transform.parent = transform;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}