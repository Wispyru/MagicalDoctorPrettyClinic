using System.Collections.Generic;
using UnityEngine;

public class MedicineMatch : MonoBehaviour
{
    private GridGeneration _gridGeneration;
    private GridCascade _gridCascade;

    private void Start()
    {
        _gridGeneration = GetComponent<GridGeneration>();
        _gridCascade = GetComponent<GridCascade>();
    }

    public bool CheckForMatches(GameObject current)
    {
        MedicineData currentData = current.GetComponent<MedicineData>();
        MedicineType targetType = currentData.Type;

        HashSet<MedicineData> horizontalMatches = GetLineMatches(currentData, targetType, true);
        HashSet<MedicineData> verticalMatches = GetLineMatches(currentData, targetType, false);

        HashSet<MedicineData> matches = new HashSet<MedicineData>();

        if (horizontalMatches.Count >= 3)
            matches.UnionWith(horizontalMatches);

        if (verticalMatches.Count >= 3)
            matches.UnionWith(verticalMatches);

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

                if (_gridGeneration.Grid[dir.x, dir.y] == null) continue;

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

    private void MatchDestroy(HashSet<MedicineData> matches)
    {
        foreach (MedicineData g in matches)
        {
            Vector2Int pos = g.GetComponent<MedicineSelect>().Position;
            _gridGeneration.Grid[pos.x, pos.y] = null;
            GameObject.Destroy(g.gameObject);
        }
        matches.Clear();

        _gridCascade.TriggerCascade();
    }

    private bool IsValid(int r, int c)
    {
        return r >= 0 && r < _gridGeneration.Width && c >= 0 && c < _gridGeneration.Height;
    }
}