using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager3D : MonoBehaviour
{
    public GameObject puzzleUI;
    public InputField inputField;
    public Text feedbackText;

    void Start() {
        puzzleUI.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            float distance = Vector3.Distance(transform.position, puzzleUI.transform.position);
            if (distance < 3f) {
                puzzleUI.SetActive(true);
            }
        }
    }

    public void CheckAnswer() {
        if (inputField.text == "5")
            feedbackText.text = "Correct!";
        else
            feedbackText.text = "Try Again!";
    }
}
