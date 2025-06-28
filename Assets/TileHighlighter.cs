using UnityEngine;

public class TileHighlighter : MonoBehaviour
{
    private TileScript tile;

    void Start()
    {
        tile = GetComponent<TileScript>();
    }

    void Update()
    {
        if (tile.tileNumber == GameManager2.Instance.nextExpectedNumber)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
}
