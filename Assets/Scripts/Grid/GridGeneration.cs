using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{
    public int Width;
    public int Height;
    public GameObject TilePrefab;
    public GameObject[,] Grid;
    public MedicineMatch Matching;

    private List<MedicineType> _enumValues;

    void Start()
    {
        Grid = new GameObject[Width, Height];
        Matching = GetComponent<MedicineMatch>();
        SetUpGrid();
    }

    /// <summary>
    /// Generates the grid where we will spawn in the medicine.
    /// </summary>
    private void SetUpGrid()
    {
        for (int column = 0; column < Width; column++)
        {
            for (int row = 0; row < Height; row++)
            {
                CheckTileMatch(column, row);
                SpawnTile(column, row);
            }
        }
    }

    /// <summary>
    /// Spawns a single tile at the given column and row, starting from the buffer position if provided.
    /// </summary>
    public void SpawnTile(int column, int row, Vector3? bufferPosition = null)
    {
        Vector3 spawnPosition = bufferPosition.HasValue
            ? bufferPosition.Value
            : new Vector3(column, row, 2f);

        GameObject newTile = Instantiate(TilePrefab, spawnPosition, Quaternion.identity, transform);
        MedicineSelect medicineSelect = newTile.AddComponent<MedicineSelect>();
        medicineSelect.Position = new Vector2Int(column, row);

        MedicineData medicineData = newTile.GetComponent<MedicineData>();
        medicineData.Type = _enumValues[Random.Range(0, _enumValues.Count)];
        medicineData.SetMedicineColor();

        if (bufferPosition.HasValue)
            newTile.GetComponent<SpriteRenderer>().enabled = false;

        newTile.name = $"({column},{row})";
        Grid[column, row] = newTile;
    }

    /// <summary>
    /// Checks the medicine tiles next to the current medicine tile to ensure no matches are made when generating grid.
    /// </summary>
    public void CheckTileMatch(int column, int row)
    {
        _enumValues = System.Enum.GetValues(typeof(MedicineType)).Cast<MedicineType>().ToList();

        GameObject left1 = GetMedicineAt(column - 1, row);
        GameObject left2 = GetMedicineAt(column - 2, row);
        if (left2 != null && left1.GetComponent<MedicineData>().Type == left2.GetComponent<MedicineData>().Type)
        {
            _enumValues.Remove(left1.GetComponent<MedicineData>().Type);
        }

        GameObject down1 = GetMedicineAt(column, row - 1);
        GameObject down2 = GetMedicineAt(column, row - 2);
        if (down2 != null && down1.GetComponent<MedicineData>().Type == down2.GetComponent<MedicineData>().Type)
        {
            _enumValues.Remove(down1.GetComponent<MedicineData>().Type);
        }
    }

    /// <summary>
    /// Gets the medicine tile at the given column and row.
    /// </summary>
    public GameObject GetMedicineAt(int column, int row)
    {
        if (column < 0 || column >= Width
            || row < 0 || row >= Height) return null;
        return Grid[column, row];
    }
}