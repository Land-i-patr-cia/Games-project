using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip buttonClickSound;
    public AudioClip backgroundMusic;
    private AudioSource[] audioSources;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        if (audioSources.Length < 2)
        {
            Debug.LogError("Two AudioSources required on MenuManager");
            return;
        }
        audioSources[0].playOnAwake = false;
        audioSources[0].volume = 1f;
        audioSources[1].clip = backgroundMusic;
        audioSources[1].loop = true;
        audioSources[1].volume = 0.5f;
        audioSources[1].Play();
        if (backgroundMusic == null)
        {
            Debug.LogError("backgroundMusic not assigned");
        }
        if (buttonClickSound == null)
        {
            Debug.LogError("buttonClickSound not assigned");
        }
    }

    public void StartGame()
    {
        if (audioSources[0] != null && buttonClickSound != null)
        {
            audioSources[0].PlayOneShot(buttonClickSound);
        }
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        if (audioSources[0] != null && buttonClickSound != null)
        {
            audioSources[0].PlayOneShot(buttonClickSound);
        }
        Application.Quit();
    }
}