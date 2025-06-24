using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEffect : MonoBehaviour
{
    private Button button;
    private RectTransform rectTransform;
    private Vector3 originalScale;

    void Start()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        StartCoroutine(ClickAnimation());
    }

    System.Collections.IEnumerator ClickAnimation()
    {
        rectTransform.localScale = new Vector3(0.9f, 0.9f, 0.9f); // Shrink
        yield return new WaitForSeconds(0.1f); // Wait 0.1 seconds
        rectTransform.localScale = originalScale; // Back to normal
    }
}