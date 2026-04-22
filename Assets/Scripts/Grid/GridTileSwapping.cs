using System.Collections;
using UnityEngine;

public class GridTileSwapping : MonoBehaviour
{
    private GridGeneration _gridGeneration;

    private void Start()
    {
        _gridGeneration = FindAnyObjectByType<GridGeneration>();
    }

    /// <summary>
    /// Swaps two tiles and reverts if no match is made.
    /// </summary>
    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        GameObject tile1 = _gridGeneration.Grid[tile1Position.x, tile1Position.y];
        GameObject tile2 = _gridGeneration.Grid[tile2Position.x, tile2Position.y];

        PerformSwap(tile1, tile2, tile1Position, tile2Position);

        bool tile1Matched = _gridGeneration.Matching.CheckForMatches(tile1);
        bool tile2Matched = _gridGeneration.Matching.CheckForMatches(tile2);

        if (!tile1Matched && !tile2Matched)
        {
            PerformSwap(tile1, tile2, tile2Position, tile1Position);
        }
    }

    /// <summary>
    /// Performs the swap of two tiles in both world space and the grid array.
    /// </summary>
    private void PerformSwap(GameObject tile1, GameObject tile2, Vector2Int tile1Position, Vector2Int tile2Position)
    {
        tile1.transform.position = _gridGeneration.GetWorldPosition(tile2Position.x, tile2Position.y);
        tile2.transform.position = _gridGeneration.GetWorldPosition(tile1Position.x, tile1Position.y);

        _gridGeneration.Grid[tile1Position.x, tile1Position.y] = tile2;
        _gridGeneration.Grid[tile2Position.x, tile2Position.y] = tile1;

        tile1.GetComponent<MedicineSelect>().Position = tile2Position;
        tile2.GetComponent<MedicineSelect>().Position = tile1Position;
    }
}