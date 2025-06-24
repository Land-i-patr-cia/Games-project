using UnityEngine;
public class Flicker : MonoBehaviour
{
    void Update()
    {
        if (Random.value < 0.05f)
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
    }
}