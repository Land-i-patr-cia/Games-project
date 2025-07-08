using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject Player;
    public GameObject[] classmates;
    public AudioClip popupSound;
    private AudioSource audioSource;
    public float popupDelay = 2f;
    public float postClickDelay = 2f;

    void Start()
    {
        Debug.Log("IntroScene Start");
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing on IntroManager. Adding one.");
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = 1f;
        }
        if (popupSound == null)
        {
            Debug.LogError("popupSound not assigned in IntroScene.cs");
        }
        if (popupPanel == null)
        {
            Debug.LogError("PopupPanel not assigned");
        }
        if (Player == null)
        {
            Debug.LogError("MainStudent not assigned");
        }
        else
        {
            Animator studentAnim = Player.GetComponent<Animator>();
            if (studentAnim == null)
            {
                Debug.LogError("Animator missing on mainStudent");
            }
        }
        if (classmates == null || classmates.Length == 0)
        {
            Debug.LogError("Classmates array empty");
        }
        popupPanel.SetActive(false);
        Invoke("ShowPopup", popupDelay);

        AudioSource ambianceSource = gameObject.AddComponent<AudioSource>();
        AudioClip ambiance = Resources.Load<AudioClip>("ClassroomAmbiance");
        if (ambiance != null)
        {
            ambianceSource.clip = ambiance;
            ambianceSource.loop = true;
            ambianceSource.volume = 0.3f;
            ambianceSource.Play();
            Debug.Log("ClassroomAmbiance playing");
        }
        else
        {
            Debug.LogError("ClassroomAmbiance not found in Resources");
        }
    }

    void ShowPopup()
    {
        Debug.Log("ShowPopup called");
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Cannot show popup: popupPanel is null");
        }
        if (audioSource != null && popupSound != null)
        {
            audioSource.PlayOneShot(popupSound, 1f);
            Debug.Log("Playing popupSound");
        }
        else
        {
            Debug.LogError($"Audio playback failed: audioSource is {(audioSource == null ? "null" : "present")}, popupSound is {(popupSound == null ? "null" : "assigned")}");
        }
    }

    public void OnPopupClick()
    {
        Debug.Log("Popup clicked");
        foreach (GameObject classmate in classmates)
        {
            if (classmate != null)
            {
                Animator anim = classmate.GetComponent<Animator>();
                if (anim != null)
                {
                    Debug.Log($"Triggering LookAt for {classmate.name}");
                    anim.SetTrigger("LookAt");
                }
                else
                {
                    Debug.LogError($"Animator missing on {classmate.name}");
                }
            }
            else
            {
                Debug.LogError("Classmate GameObject is null in classmates array");
            }
        }
        if (Player != null)
        {
            Animator studentAnim = Player.GetComponent<Animator>();
            if (studentAnim != null)
            {
                Debug.Log("Triggering SuckedIn for mainStudent");
                studentAnim.SetTrigger("SuckedIn");
            }
            else
            {
                Debug.LogError("Animator missing on mainStudent");
            }
        }
        else
        {
            Debug.LogError("mainStudent is null");
        }
        Invoke("LoadLevel1", postClickDelay);
    }

    void LoadLevel1()
    {
        Debug.Log("LoadLevel1 called");
        SceneManager.LoadScene("Menu Scene");
    }
}