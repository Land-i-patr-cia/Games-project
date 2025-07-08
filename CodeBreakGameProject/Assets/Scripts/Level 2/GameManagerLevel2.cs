using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerLevel2 : MonoBehaviour
{
    public TextMeshProUGUI collectibleText;
    public CanvasGroup riddlePanel;
    public TMP_InputField inputField;
    public TextMeshProUGUI riddleText;
    public AudioClip successSound;
    private AudioSource audioSource;
    private int collectiblesCollected = 0;
    private int currentRiddleIndex = 0;
    private string[] riddles = { "What manages hardware during boot? (Hint: Basic Input/Output)", "What loads the OS? (Hint: Core of the system)" };
    private string[] answers = { "bios", "kernel" };

    void Start()
    {
        Debug.Log("GameManagerLevel2 Start");
        if (collectibleText == null) Debug.LogError("collectibleText not assigned");
        if (riddlePanel == null) Debug.LogError("riddlePanel not assigned");
        if (inputField == null) Debug.LogError("inputField not assigned");
        if (riddleText == null) Debug.LogError("riddleText not assigned");
        if (successSound == null) Debug.LogError("successSound not assigned");
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("AudioSource missing");
        UpdateCollectibleText();
        riddlePanel.alpha = 0f;
        riddlePanel.blocksRaycasts = false;
        riddlePanel.interactable = false;
    }

    public void CollectibleCollected()
    {
        collectiblesCollected++;
        Debug.Log($"Collectible collected: {collectiblesCollected}/3");
        UpdateCollectibleText();
        if (collectiblesCollected >= 3)
        {
            ShowRiddle();
        }
    }

    void UpdateCollectibleText()
    {
        if (collectibleText != null)
        {
            collectibleText.text = $"Collectibles: {collectiblesCollected}/3";
        }
    }

    void ShowRiddle()
    {
        if (currentRiddleIndex < riddles.Length)
        {
            riddleText.text = riddles[currentRiddleIndex];
            riddlePanel.alpha = 1f;
            riddlePanel.blocksRaycasts = true;
            riddlePanel.interactable = true;
            inputField.text = "";
            inputField.ActivateInputField();
        }
        else
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public void SubmitAnswer()
    {
        if (inputField.text.ToLower() == answers[currentRiddleIndex])
        {
            Debug.Log("Correct answer: " + answers[currentRiddleIndex]);
            if (audioSource != null && successSound != null)
            {
                audioSource.PlayOneShot(successSound);
            }
            currentRiddleIndex++;
            ShowRiddle();
        }
        else
        {
            Debug.Log("Incorrect answer: " + inputField.text);
        }
    }
}