using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int tileNumber; // Assigned in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager2.Instance.TileSteppedOn(this);
        }
    }
}

