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

        for (int row = 0; row < Width; row++)
        {
            for (int column = 0; column < Height; column++)
            {
                CheckTileMatch(column, row);
                Vector3 temporaryPosition = new Vector3(column, row, 2f);
                GameObject newTile = Instantiate(TilePrefab, temporaryPosition, Quaternion.identity, transform);
                MedicineSelect medicineSelect = newTile.AddComponent<MedicineSelect>();
                medicineSelect.Position = new Vector2Int(column, row);

                newTile.GetComponent<MedicineData>().Type = _enumValues[Random.Range(0, _enumValues.Count)];
                newTile.GetComponent<MedicineData>().SetMedicineColor();

                newTile.name = $"({column},{row})";
                Grid[column, row] = newTile;
            }
        }

    }

    /// <summary>
    /// Checks the medicine tiles next to the current medicine tile to ensure no matches are made when generating grid.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    public void CheckTileMatch(int column, int row)
    {
        GameObject left1 = GetMedicineAt(column - 1, row);
        GameObject left2 = GetMedicineAt(column - 2, row);
        _enumValues = System.Enum.GetValues(typeof(MedicineType)).Cast<MedicineType>().ToList();
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
    /// Gets the medicine tile.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    public GameObject GetMedicineAt(int column, int row)
    {
        if(column < 0 || column >= Height 
            || row < 0 || row >= Width) return null;
        GameObject tile = Grid[column, row];

        return tile;
    }



    


}
