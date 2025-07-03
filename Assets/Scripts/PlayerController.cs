using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public float speed = 0;

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
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Debug.Log("OnMove called");
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        // If carrying, keep collectible with player
        if (carriedCollectible != null)
        {
            carriedCollectible.transform.position = transform.position + new Vector3(0, 1, 0); // Offset above player
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pick up collectible if not carrying
        if (carriedCollectible == null && other.CompareTag("PickUp"))
        {
            carriedCollectible = other.gameObject;
            carriedType = carriedCollectible.GetComponent<Collectible>().type; // e.g. "Green", "Red", "Yellow"
            carriedCollectible.GetComponent<Collider>().enabled = false;
            // Optionally, disable renderer on ground or set as child
        }
        // Deposit logic
        else if (carriedCollectible != null)
        {
            // Main Memory (Green)
            if (other.CompareTag("MainMemory") && carriedType == "Green")
            {
                DepositCollectible(ref mainMemoryCount, mainMemoryTotal);
            }
            // RAM (Red)
            else if (other.CompareTag("RAM") && carriedType == "Red")
            {
                DepositCollectible(ref ramCount, ramTotal);
            }
            // CPU (Yellow)
            else if (other.CompareTag("CPU") && carriedType == "Yellow")
            {
                DepositCollectible(ref cpuCount, cpuTotal);
            }
        }
    }

    void DepositCollectible(ref int count, int total)
    {
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

    void SetCountText()
    {
        countText.text = $"CPU / Cache data: {cpuCount}/{cpuTotal}\nRAM data: {ramCount}/{ramTotal}\nMain Memory data: {mainMemoryCount}/{mainMemoryTotal}";
    }
}
