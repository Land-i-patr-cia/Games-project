using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Shake(float duration)
    {
        InvokeRepeating(nameof(DoShake), 0, 0.1f);
        Invoke(nameof(StopShake), duration);
    }

    void DoShake()
    {
        Vector3 pos = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), -5);
        transform.position = pos;
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        transform.position = new Vector3(0, 2, -5);
    }
}