using UnityEngine;

public class Fan : MonoBehaviour
{
    public float pushForce = 10f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * pushForce, ForceMode.Force);
            }
        }
    }
}