using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class MedicineMatch : MonoBehaviour
{
    private GridGeneration _gridGeneration;
    private MedicineSelect _select;

    private List<GameObject> matchedColumnTiles;
    private List<GameObject> matchedRowTiles;

    private void Start()
    {
        _gridGeneration = GetComponent<GridGeneration>();
        matchedColumnTiles = new List<GameObject>();
        matchedRowTiles = new List<GameObject>();
    }
    
    public void CheckForMatches()
    {
        for(int row = 0;  row < _gridGeneration.Width; row++)
        {
            for (int column = 0; column < _gridGeneration.Height; column++)
            {
                GameObject currentTile = _gridGeneration.GetMedicineAt(column, row);
                matchedColumnTiles = FindColumnMatches(column, row, currentTile);

            }
        }
    }

    public List<GameObject> FindColumnMatches(int column, int row, GameObject tile)
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = column + 1; i < _gridGeneration.Height; i++)
        {
            GameObject nextColumn = _gridGeneration.GetMedicineAt(i, row);
      
        }

        return result;
    }

   /* public List<GameObject> FindRowMatches(int column, int row, GameObject tile)
    {
        for(int i = column + 1; i < _gridGeneration.Width; i++)
        {
            GameObject nextRow = _gridGeneration.GetMedicineAt(column, i);

        }
    }*/

}
