using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    public int nextExpectedNumber = 1; // Start at 1
    public int totalTiles = 6; // Number of tiles in the puzzle

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TileSteppedOn(TileScript tile)
    {
        if (tile.tileNumber == nextExpectedNumber)
        {
            Debug.Log("Correct tile: " + tile.tileNumber);
            nextExpectedNumber++;

            // Change tile color to indicate success
            tile.GetComponent<Renderer>().material.color = Color.green;

            if (nextExpectedNumber > totalTiles)
            {
                Debug.Log("Puzzle Completed!");
                // Add victory logic here once designer (P) is done
             }
        }
        else
        {
            Debug.Log("Incorrect tile! Resetting puzzle.");
            nextExpectedNumber = 1;
            ResetTiles();
        }
    }

    private void ResetTiles()
    {
        TileScript[] tiles = FindObjectsOfType<TileScript>();
        foreach (TileScript t in tiles)
        {
            t.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
