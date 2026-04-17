using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCascade : MonoBehaviour
{
    [SerializeField]
    private float _fallDuration = 0.3f;

    private GridGeneration _gridGeneration;
    private MedicineMatch _medicineMatch;

    private void Start()
    {
        _gridGeneration = GetComponent<GridGeneration>();
        _medicineMatch = GetComponent<MedicineMatch>();
    }

    /// <summary>
    /// Triggers the full cascade: drops tiles down, fills empty slots, then checks for chain matches.
    /// </summary>
    public void TriggerCascade()
    {
        StartCoroutine(CascadeRoutine());
    }

    /// <summary>
    /// Handles the full cascade sequence, blocking input during animation.
    /// </summary>
    private IEnumerator CascadeRoutine()
    {
        GameData.IsAnimating = true;

        yield return StartCoroutine(DropTiles());

        FillEmptySlots();

        yield return StartCoroutine(WaitForFill());

        GameData.IsAnimating = false;

        CheckCascadeMatches();
    }

    /// <summary>
    /// Drops existing tiles down to fill empty slots in each column, animating the movement.
    /// </summary>
    private IEnumerator DropTiles()
    {
        List<(GameObject tile, Vector3 startPos, Vector3 endPos)> fallingTiles 
            = new List<(GameObject, Vector3, Vector3)>();

        for (int column = 0; column < _gridGeneration.Width; column++)
        {
            for (int row = 0; row < _gridGeneration.Height; row++)
            {
                if (_gridGeneration.Grid[column, row] != null) continue;

                for (int rowAbove = row + 1; rowAbove < _gridGeneration.Height; rowAbove++)
                {
                    if (_gridGeneration.Grid[column, rowAbove] == null) continue;

                    GameObject tile = _gridGeneration.Grid[column, rowAbove];
                    Vector3 targetPos = new Vector3(column, row, 2f);

                    // Update grid array and tile data
                    _gridGeneration.Grid[column, row] = tile;
                    _gridGeneration.Grid[column, rowAbove] = null;
                    tile.GetComponent<MedicineSelect>().Position = new Vector2Int(column, row);
                    tile.name = $"({column},{row})";

                    fallingTiles.Add((tile, tile.transform.position, targetPos));
                    break;
                }
            }
        }

        yield return StartCoroutine(AnimateFall(fallingTiles));
    }

    /// <summary>
    /// Animates all falling tiles simultaneously from their start to end positions.
    /// </summary>
    private IEnumerator AnimateFall(List<(GameObject tile, Vector3 startPos, Vector3 endPos)> fallingTiles)
    {
        float elapsed = 0f;

        while (elapsed < _fallDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _fallDuration);

            foreach (var (tile, startPos, endPos) in fallingTiles)
            {
                if (tile == null) continue;
                tile.transform.position = Vector3.Lerp(startPos, endPos, t);
            }

            yield return null;
        }

        // Snap to final positions
        foreach (var (tile, _, endPos) in fallingTiles)
        {
            if (tile == null) continue;
            tile.transform.position = endPos;
        }
    }

    /// <summary>
    /// Fills any remaining empty slots at the top of each column with new tiles.
    /// </summary>
    private void FillEmptySlots()
    {
        for (int column = 0; column < _gridGeneration.Width; column++)
        {
            for (int row = 0; row < _gridGeneration.Height; row++)
            {
                if (_gridGeneration.Grid[column, row] != null) continue;

                _gridGeneration.CheckTileMatch(column, row);
                _gridGeneration.SpawnTile(column, row);
            }
        }
    }

    /// <summary>
    /// Small wait to ensure newly spawned tiles are ready before checking matches.
    /// </summary>
    private IEnumerator WaitForFill()
    {
        yield return null;
    }

    /// <summary>
    /// Checks all tiles for matches after cascading, handling chain reactions.
    /// </summary>
    private void CheckCascadeMatches()
    {
        bool anyMatchFound = false;

        for (int column = 0; column < _gridGeneration.Width; column++)
        {
            for (int row = 0; row < _gridGeneration.Height; row++)
            {
                if (_gridGeneration.Grid[column, row] == null) continue;

                bool matchFound = _medicineMatch.CheckForMatches(_gridGeneration.Grid[column, row]);
                if (matchFound) anyMatchFound = true;
            }
        }

        if (anyMatchFound)
            TriggerCascade();
    }
}