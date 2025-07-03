using UnityEngine;
using System.Collections.Generic;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    private List<int> evenNumbers = new List<int>();
    private List<int> oddNumbers = new List<int>();
    private bool doingEvens = true;
    private int currentIndex = 0;

    public int totalTiles = 6; // (Optional, for reference)

    private void Start()
    {
        // Get all tile numbers in the scene
        TileScript[] tiles = FindObjectsOfType<TileScript>();

        foreach (TileScript tile in tiles)
        {
            if (tile.tileNumber % 2 == 0)
                evenNumbers.Add(tile.tileNumber);
            else
                oddNumbers.Add(tile.tileNumber);
        }

        evenNumbers.Sort();
        oddNumbers.Sort();

        HighlightNextTile();
    }

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
        int? expectedNumber = GetCurrentExpectedNumber();

        if (expectedNumber.HasValue && tile.tileNumber == expectedNumber.Value)
        {
            Debug.Log("Correct tile: " + tile.tileNumber);
            tile.GetComponent<Renderer>().material.color = Color.green;
            currentIndex++;

            if (doingEvens && currentIndex >= evenNumbers.Count)
            {
                // Switch to odd numbers
                doingEvens = false;
                currentIndex = 0;
            }

            // Check if completed
            if (!doingEvens && currentIndex >= oddNumbers.Count)
            {
                Debug.Log("Puzzle Completed!");
            }

            HighlightNextTile();
        }
        else
        {
            Debug.Log("Incorrect tile! Resetting puzzle.");
            ResetTiles();
        }
    }

    private int? GetCurrentExpectedNumber()
    {
        if (doingEvens)
        {
            if (currentIndex < evenNumbers.Count)
                return evenNumbers[currentIndex];
        }
        else
        {
            if (currentIndex < oddNumbers.Count)
                return oddNumbers[currentIndex];
        }

        return null;
    }

    private void HighlightNextTile()
    {
        int? expected = GetCurrentExpectedNumber();

        TileScript[] tiles = FindObjectsOfType<TileScript>();
        foreach (TileScript t in tiles)
        {
            Renderer rend = t.GetComponent<Renderer>();

            if (rend.material.color == Color.green)
                continue; // Keeps correct tiles green

            if (expected.HasValue && t.tileNumber == expected.Value)
            {
                rend.material.color = Color.white; // was yellow but now white cause too easy
            }
            else
            {
                rend.material.color = Color.white;
            }
        }
    }

    private void ResetTiles()
    {
        doingEvens = true;
        currentIndex = 0;

        TileScript[] tiles = FindObjectsOfType<TileScript>();
        foreach (TileScript t in tiles)
        {
            t.GetComponent<Renderer>().material.color = Color.white;
        }

        HighlightNextTile();
    }
}
