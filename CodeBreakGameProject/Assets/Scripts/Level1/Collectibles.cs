using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound;
    private AudioSource audioSource;

    void Start()
    {
        Debug.Log("Collectible Start for " + gameObject.name);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("AudioSource missing on " + gameObject.name);
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("isSpinning", true);
        }
        else
        {
            Debug.LogError("Animator missing on " + gameObject.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collectible triggered by Player: " + gameObject.name);
            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            else
            {
                Debug.LogWarning("AudioSource or collectSound missing on " + gameObject.name);
            }
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.CollectibleCollected();
                Debug.Log("CollectibleCollected called on GameManager");
            }
            else
            {
                Debug.LogError("GameManager not found in scene");
            }
            Destroy(gameObject);
        }
    }
}