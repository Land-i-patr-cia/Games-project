using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    void Start()
    {
        Debug.Log("WinScene Start");
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame called, loading MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}