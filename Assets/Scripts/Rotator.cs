using UnityEngine;

public class Rotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(10, 20, 10), 20 * Time.deltaTime);
    }
}
