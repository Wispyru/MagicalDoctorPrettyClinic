using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCascade : MonoBehaviour
{
    [SerializeField]
    private float _fallSpeed = 10f;

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

        List<(GameObject tile, Vector3 startPos, Vector3 endPos)> fallingTiles = CompactColumns();

        FillEmptySlots(fallingTiles);

        yield return StartCoroutine(AnimateFall(fallingTiles));

        GameData.IsAnimating = false;

        CheckCascadeMatches();
    }

    /// <summary>
    /// Compacts each column by shifting all existing tiles down to fill gaps.
    /// Returns a list of tiles that need to be animated.
    /// </summary>
    private List<(GameObject tile, Vector3 startPos, Vector3 endPos)> CompactColumns()
    {
        List<(GameObject tile, Vector3 startPos, Vector3 endPos)> fallingTiles
            = new List<(GameObject, Vector3, Vector3)>();

        for (int column = 0; column < _gridGeneration.Width; column++)
        {
            List<GameObject> columnTiles = new List<GameObject>();
            for (int row = 0; row < _gridGeneration.Height; row++)
            {
                if (_gridGeneration.Grid[column, row] != null)
                    columnTiles.Add(_gridGeneration.Grid[column, row]);
            }

            for (int row = 0; row < _gridGeneration.Height; row++)
                _gridGeneration.Grid[column, row] = null;

            for (int i = 0; i < columnTiles.Count; i++)
            {
                GameObject tile = columnTiles[i];
                Vector3 startPos = tile.transform.position;
                Vector3 endPos = _gridGeneration.GetWorldPosition(column, i);

                _gridGeneration.Grid[column, i] = tile;
                tile.GetComponent<MedicineSelect>().Position = new Vector2Int(column, i);
                tile.name = $"({column},{i})";

                if (startPos != endPos)
                    fallingTiles.Add((tile, startPos, endPos));
            }
        }

        return fallingTiles;
    }

    /// <summary>
    /// Fills any remaining empty slots at the top of each column with new buffer tiles.
    /// </summary>
    private void FillEmptySlots(List<(GameObject tile, Vector3 startPos, Vector3 endPos)> fallingTiles)
    {
        for (int column = 0; column < _gridGeneration.Width; column++)
        {
            List<int> emptyRows = new List<int>();
            for (int row = 0; row < _gridGeneration.Height; row++)
            {
                if (_gridGeneration.Grid[column, row] == null)
                    emptyRows.Add(row);
            }

            int emptyCount = emptyRows.Count;
            if (emptyCount == 0) continue;

            for (int i = 0; i < emptyCount; i++)
            {
                int targetRow = emptyRows[i];

                Vector3 spawnPos = _gridGeneration.GetWorldPosition(column, _gridGeneration.Height + i);
                Vector3 endPos = _gridGeneration.GetWorldPosition(column, targetRow);

                _gridGeneration.CheckTileMatch(column, targetRow);
                _gridGeneration.SpawnTile(column, targetRow, bufferPosition: spawnPos);

                GameObject newTile = _gridGeneration.Grid[column, targetRow];
                fallingTiles.Add((newTile, spawnPos, endPos));
            }
        }
    }

    /// <summary>
    /// Animates all falling tiles simultaneously, each at a speed proportional to the distance they travel.
    /// </summary>
    private IEnumerator AnimateFall(List<(GameObject tile, Vector3 startPos, Vector3 endPos)> fallingTiles)
    {
        foreach (var (tile, _, _) in fallingTiles)
        {
            if (tile == null) continue;
            tile.GetComponent<SpriteRenderer>().enabled = true;
        }

        List<float> durations = new List<float>();
        float longestDuration = 0f;

        foreach (var (tile, startPos, endPos) in fallingTiles)
        {
            float distance = Vector3.Distance(startPos, endPos);
            float duration = distance / _fallSpeed;
            durations.Add(duration);

            if (duration > longestDuration)
                longestDuration = duration;
        }

        float elapsed = 0f;

        while (elapsed < longestDuration)
        {
            elapsed += Time.deltaTime;

            for (int i = 0; i < fallingTiles.Count; i++)
            {
                var (tile, startPos, endPos) = fallingTiles[i];
                if (tile == null) continue;

                float t = Mathf.Clamp01(elapsed / durations[i]);
                tile.transform.position = Vector3.Lerp(startPos, endPos, t);
            }

            yield return null;
        }

        foreach (var (tile, _, endPos) in fallingTiles)
        {
            if (tile == null) continue;
            tile.transform.position = endPos;
        }
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