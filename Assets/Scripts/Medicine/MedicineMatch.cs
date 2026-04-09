using System;
using System.Collections.Generic;
using UnityEngine;

public class MedicineMatch : MonoBehaviour
{
    private GridGeneration _gridGeneration;
    
    private List<GameObject> matchedColumnTiles;
    private List<GameObject> matchedRowTiles;

    private void Start()
    {
        _gridGeneration = GetComponent<GridGeneration>();
        matchedColumnTiles = new List<GameObject>();
        matchedRowTiles = new List<GameObject>();  
        
    }
    private void FindColumnMatches(int column, int row, GameObject currentTile)
    {
        List<GameObject> temp = new List<GameObject>();
        for (int i = column + 1; i < _gridGeneration.Height; i ++)
        {
            GameObject nextColumn = _gridGeneration.GetMedicineAt(column, i);
            if (nextColumn)
            {
                
            }
        }
    }
}
