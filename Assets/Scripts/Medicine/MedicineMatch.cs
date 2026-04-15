using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

public class MedicineMatch : MonoBehaviour
{
    private GridGeneration _gridGeneration;

    private void Start()
    {
        _gridGeneration = GetComponent<GridGeneration>();
    }
    
    public void CheckForMatches(GameObject current)
    {
        Debug.Log("reached.");


        HashSet<MedicineData> matches = new HashSet<MedicineData>();
        Stack<MedicineData> checkedList = new Stack<MedicineData>();

        MedicineData currentData = current.GetComponent<MedicineData>();

        checkedList.Push(currentData);
        if (!matches.Contains(currentData))
        {
            matches.Add(currentData);
        }
        
        while(checkedList.TryPop(out var target))
        {
            MedicineType targetType = target.Type;
            Debug.Log(target.ToString());
            List<MedicineData> neighbours = GetNeighbours(target.transform);
            foreach (MedicineData x in neighbours)
            {
                if (checkedList.TryPop(out target)) continue;
                checkedList.Push(x);

                if (x.Type == targetType)
                {
                    matches.Add(x);
                    break;
                }
            }
        }

        if (matches.Count >= 3)
        {
            MatchDestroy(matches); 
        }
            
    }



    private List<MedicineData> GetNeighbours(Transform current)
    {
        int x = (int)current.position.x;
        int y = (int)current.position.y;

        if (!IsValid(x, y)) return null;

        List<MedicineData> collectedNeighbors = new List<MedicineData>();

        Vector2Int[] directions = {
        new Vector2Int(x, y + 1), // up
        new Vector2Int(x, y - 1), // down
        new Vector2Int(x - 1, y),     // left
        new Vector2Int(x + 1, y),     // right
    };

        foreach (Vector2Int dir in directions)
        {
            if (IsValid(dir.x, dir.y))
                TryAddNeighbour(dir.x, dir.y, collectedNeighbors);
        }

        return collectedNeighbors;
    }

    private void TryAddNeighbour(int x, int y, List<MedicineData> neighbours)
    {
        MedicineData neighbour = _gridGeneration.Grid[x, y].GetComponent<MedicineData>();
        Debug.Log($"neighbour is = {neighbour}");
        neighbours.Add(neighbour);
    }

    private void MatchDestroy(HashSet<MedicineData> matches )
    {
        foreach (MedicineData g in matches)
        {
            Debug.Log(g);
            break;
        }
        
    }

    private bool IsValid(int r, int c)
    {
        return r >= 0 && r < _gridGeneration.Width && c >= 0 && c < _gridGeneration.Height;
    }
}
