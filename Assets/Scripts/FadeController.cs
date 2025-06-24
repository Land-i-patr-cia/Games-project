using UnityEngine;
using UnityEngine.UIElements;

public class FadeController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement fadePanel;
    private bool isFading = false;
    private float fadeSpeed = 1.0f;
    private float targetOpacity = 0f;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            fadePanel = uiDocument.rootVisualElement;
        }
        else
        {
            Debug.LogError("FadeController: No UIDocument found!");
        }
        FadeIn(); // Fade in when starting
    }

    public void FadeOut()
    {
        if (!isFading)
        {
            isFading = true;
            targetOpacity = 1f; // Fade to black
        }
    }

    public void FadeIn()
    {
        if (!isFading)
        {
            isFading = true;
            targetOpacity = 0f; // Fade to clear
        }
    }

    void Update()
    {
        if (isFading && fadePanel != null)
        {
            float currentOpacity = fadePanel.style.opacity.value;
            currentOpacity = Mathf.Lerp(currentOpacity, targetOpacity, Time.deltaTime * fadeSpeed);
            fadePanel.style.opacity = new StyleFloat(currentOpacity);

            if (Mathf.Abs(currentOpacity - targetOpacity) < 0.01f)
            {
                fadePanel.style.opacity = new StyleFloat(targetOpacity);
                isFading = false;
            }
        }
    }
}