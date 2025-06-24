using UnityEngine;
using UnityEngine.UIElements;

public class FadeAnimator : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement fadePanel;
    public float opacity = 0f;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        fadePanel = uiDocument.rootVisualElement;
    }

    void Update()
    {
        fadePanel.style.opacity = opacity;
    }
}