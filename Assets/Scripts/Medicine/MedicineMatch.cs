using System.Collections.Generic;
using UnityEngine;

public class MedicineMatch : MonoBehaviour
{
    private GridGeneration _gridGeneration;
    private GridTileSwapping _tileSwapping;

    private void Start()
    {
        _gridGeneration = GetComponent<GridGeneration>();
        _tileSwapping = GetComponent<GridTileSwapping>();
    }

    public bool CheckForMatches(GameObject current)
    {
        Debug.Log("Running check for matches");

        MedicineData currentData = current.GetComponent<MedicineData>();
        MedicineType targetType = currentData.Type;

        HashSet<MedicineData> horizontalMatches = GetLineMatches(currentData, targetType, true);
        HashSet<MedicineData> verticalMatches = GetLineMatches(currentData, targetType, false);

        HashSet<MedicineData> matches = new HashSet<MedicineData>();

        if (horizontalMatches.Count >= 3)
            matches.UnionWith(horizontalMatches);

        if (verticalMatches.Count >= 3)
            matches.UnionWith(verticalMatches);

        Debug.Log($"Total matches found: {matches.Count}");

        if (matches.Count >= 3)
        {
            MatchDestroy(matches);
            return true;
        }

        return false;
    }

    private HashSet<MedicineData> GetLineMatches(MedicineData origin, MedicineType targetType, bool horizontal)
    {
        HashSet<MedicineData> matches = new HashSet<MedicineData>();
        Stack<MedicineData> toCheck = new Stack<MedicineData>();

        matches.Add(origin);
        toCheck.Push(origin);

        while (toCheck.Count > 0)
        {
            MedicineData target = toCheck.Pop();

            int x = (int)target.transform.position.x;
            int y = (int)target.transform.position.y;

            Vector2Int[] directions = horizontal
                ? new[] { new Vector2Int(x + 1, y), new Vector2Int(x - 1, y) }
                : new[] { new Vector2Int(x, y + 1), new Vector2Int(x, y - 1) };

            foreach (Vector2Int dir in directions)
            {
                if (!IsValid(dir.x, dir.y)) continue;

                MedicineData neighbour = _gridGeneration.Grid[dir.x, dir.y].GetComponent<MedicineData>();

                if (neighbour == null || matches.Contains(neighbour)) continue;

                if (neighbour.Type == targetType)
                {
                    matches.Add(neighbour);
                    toCheck.Push(neighbour);
                }
            }
        }

        return matches;
    }

    private List<MedicineData> GetNeighbours(Transform current)
    {
        int x = (int)current.position.x;
        int y = (int)current.position.y;

        if (!IsValid(x, y)) return null;

        List<MedicineData> collectedNeighbors = new List<MedicineData>();

        Vector2Int[] directions = {
            new Vector2Int(x,     y + 1), // up
            new Vector2Int(x,     y - 1), // down
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
        neighbours.Add(neighbour);
    }

    private void MatchDestroy(HashSet<MedicineData> matches)
    {
        foreach (MedicineData g in matches)
        {
            GameObject.Destroy(g.gameObject);
        }
        matches.Clear();
    }

    private bool IsValid(int r, int c)
    {
        return r >= 0 && r < _gridGeneration.Width && c >= 0 && c < _gridGeneration.Height;
    }
}