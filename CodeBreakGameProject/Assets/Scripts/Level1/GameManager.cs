using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int collectiblesNeeded = 2;
    public string correctAnswer = "boot";
    public TextMeshProUGUI collectibleText;
    public GameObject riddlePanel;
    public TMP_InputField inputField;
    private int collectiblesCollected = 0;
    public AudioClip successSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource missing on GameManager");
        }
        UpdateCollectibleText();
        riddlePanel.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void CollectibleCollected()
    {
        collectiblesCollected++;
        UpdateCollectibleText();
        if (collectiblesCollected >= collectiblesNeeded)
        {
            riddlePanel.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void UpdateCollectibleText()
    {
        if (collectibleText != null)
        {
            collectibleText.text = $"Collectibles: {collectiblesCollected}/{collectiblesNeeded}";
        }
    }

    public void SubmitAnswer()
    {
        if (inputField.text.ToLower() == correctAnswer)
        {
            if (audioSource != null && successSound != null)
            {
                audioSource.PlayOneShot(successSound);
            }
            SceneManager.LoadScene("Level 2");
        }
    }
}