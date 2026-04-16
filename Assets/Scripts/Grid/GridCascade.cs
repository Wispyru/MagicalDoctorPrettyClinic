using System.Collections.Generic;
using UnityEngine;

public class GridCascade : MonoBehaviour
{
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
        DropTiles();
        FillEmptySlots();
        CheckCascadeMatches();
    }

    /// <summary>
    /// Drops existing tiles down to fill empty slots in each column.
    /// </summary>
    private void DropTiles()
    {
        for (int column = 0; column < _gridGeneration.Width; column++)
        {
            for (int row = 0; row < _gridGeneration.Height; row++)
            {
                if (_gridGeneration.Grid[column, row] != null) continue;

                // Find the nearest tile above and drop it down
                for (int rowAbove = row + 1; rowAbove < _gridGeneration.Height; rowAbove++)
                {
                    if (_gridGeneration.Grid[column, rowAbove] == null) continue;

                    GameObject tile = _gridGeneration.Grid[column, rowAbove];

                    // Move tile down in the grid array
                    _gridGeneration.Grid[column, row] = tile;
                    _gridGeneration.Grid[column, rowAbove] = null;

                    // Move tile down in world space
                    tile.transform.position = new Vector3(column, row, 2f);
                    tile.GetComponent<MedicineSelect>().Position = new Vector2Int(column, row);
                    tile.name = $"({column},{row})";

                    break;
                }
            }
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

        // If any matches were found, cascade again to handle chain reactions
        if (anyMatchFound)
            TriggerCascade();
    }
}