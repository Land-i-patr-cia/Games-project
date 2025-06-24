using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject popupCanvas;
    public Button malwareButton;
    public AudioSource glitchSound;
    public GameObject glitchQuad;
    public GameObject fadePanel;
    private FadeController fadeController;
    public CameraShake cameraShake;
    public GameObject mainCharacter;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (popupCanvas != null) popupCanvas.SetActive(false); // Ensure popup starts hidden
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "InitialScene")
        {
            SetupInitialScene();
        }
    }

    void OnDestroy()
    {
        popupCanvas = null;
        malwareButton = null;
        glitchSound = null;
        glitchQuad = null;
        fadePanel = null;
        fadeController = null;
    }

    void SetupInitialScene()
    {
        popupCanvas = GameObject.Find("PopupCanvas");
        if (popupCanvas != null)
        {
            malwareButton = popupCanvas.transform.Find("PopupPanel/MalwareButton").GetComponent<Button>();
            fadePanel = GameObject.Find("FadePanel");
            fadeController = fadePanel.GetComponent<FadeController>();
            Invoke(nameof(ShowPopup), 2f); // Delay popup
            if (malwareButton != null) malwareButton.onClick.AddListener(OnMalwareButtonClicked);
            else Debug.LogError("MalwareButton not found!");
        }
        else
        {
            Debug.LogError("PopupCanvas not found!");
        }
    }

    void ShowPopup()
    {
        if (popupCanvas != null)
        {
            popupCanvas.SetActive(true);
            Debug.Log("Popup shown after 2 seconds");
        }
    }

    void OnMalwareButtonClicked()
    {
        Debug.Log("Button clicked!"); // Test if this prints
        if (glitchSound != null) glitchSound.Play();
        if (glitchQuad != null) glitchQuad.SetActive(true);
        if (cameraShake != null) cameraShake.Shake(1f);
        if (fadeController != null) fadeController.FadeOut();
        if (mainCharacter != null) StartCoroutine(SwallowCharacter());
        Invoke(nameof(LoadTitleScreen), 1f);
    }

    System.Collections.IEnumerator SwallowCharacter()
    {
        Vector3 startPos = mainCharacter.transform.position;
        Vector3 endPos = new Vector3(0, 0.5f, 1.1f); // Inside the computer
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            mainCharacter.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        mainCharacter.SetActive(false); // Character disappears
    }

    void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
        popupCanvas = null;
        malwareButton = null;
        glitchSound = null;
        glitchQuad = null;
        fadePanel = null;
        fadeController = null;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InitialScene")
        {
            SetupInitialScene();
        }
        else
        {
            popupCanvas = null;
            malwareButton = null;
            glitchSound = null;
            glitchQuad = null;
            fadePanel = null;
            fadeController = null;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}