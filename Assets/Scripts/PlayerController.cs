using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public float speed = 5;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // Carrying collectible
    private GameObject carriedCollectible = null;
    private string carriedType = "";

    // Deposit counts
    private int cpuCount = 0;
    private int ramCount = 0;
    private int mainMemoryCount = 0;

    // Required totals
    public int cpuTotal = 3;
    public int ramTotal = 4;
    public int mainMemoryTotal = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY) * speed;
        rb.linearVelocity = movement;

        // Carry collectible beside player and keep upright
        if (carriedCollectible != null)
        {
            carriedCollectible.transform.position = transform.position + transform.up * 1.0f + Vector3.up * 0.5f;
            carriedCollectible.transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // End or restart the game
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            // Or show a "Game Over" UI, etc.
            return;
        }
        // Pick up collectible if not carrying
        if (carriedCollectible == null && other.CompareTag("PickUp"))
        {
            carriedCollectible = other.gameObject;
            var collectibleScript = carriedCollectible.GetComponent<Collectible>();
            carriedType = collectibleScript != null ? collectibleScript.type : "";

            carriedCollectible.transform.SetParent(transform);
            var rbCollectible = carriedCollectible.GetComponent<Rigidbody>();
            if (rbCollectible != null)
            {
                rbCollectible.isKinematic = true;
                rbCollectible.freezeRotation = true;
            }
        }
        // Ignore other collectibles while carrying one
        else if (carriedCollectible != null && other.CompareTag("PickUp"))
        {
            return;
        }
        // Deposit logic
        else if (carriedCollectible != null)
        {
            if (other.CompareTag("MainMemory") && carriedType == "Green")
            {
                DepositAndDestroy(ref mainMemoryCount, mainMemoryTotal);
            }
            else if (other.CompareTag("Ram") && carriedType == "Red")
            {
                DepositAndDestroy(ref ramCount, ramTotal);
            }
            else if (other.CompareTag("Cpu") && carriedType == "Yellow")
            {
                DepositAndDestroy(ref cpuCount, cpuTotal);
            }
        }
    }

    void DepositAndDestroy(ref int count, int total)
    {
        if (carriedCollectible != null)
        {
            carriedCollectible.transform.SetParent(null);
            carriedCollectible.GetComponent<Collider>().enabled = false;
            Destroy(carriedCollectible);
            carriedCollectible = null;
            carriedType = "";
            count++;
            SetCountText();
            if (cpuCount >= cpuTotal && ramCount >= ramTotal && mainMemoryCount >= mainMemoryTotal)
            {
                winTextObject.SetActive(true);
            }
        }
    }

    void SetCountText()
    {
        countText.text = $"CPU / Cache data: {cpuCount}/{cpuTotal}\nRAM data: {ramCount}/{ramTotal}\nMain Memory data: {mainMemoryCount}/{mainMemoryTotal}";
    }
}