using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineMatch : MonoBehaviour
{
    [SerializeField]
    private LevelData _levelData;

    private GridGeneration _gridGeneration;
    private GridCascade _gridCascade;
    private GameUI _gameUI;

    private int _matchComboCount;
    private Coroutine _comboTimerCoroutine;

    private void Start()
    {
        _gridGeneration = GetComponent<GridGeneration>();
        _gridCascade = GetComponent<GridCascade>();
        _gameUI = FindAnyObjectByType<GameUI>();

        _matchComboCount = 1;
        GameData.CurrentMoves = _levelData.MaxMoves;
        GameData.MaxMoves = _levelData.MaxMoves;
    }

    /// <summary>
    /// Checks if the given tile is part of a match of 3 or more and destroys them if so.
    /// </summary>
    public bool CheckForMatches(GameObject current, bool fromPlayer = false)
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
            if (fromPlayer)
                DecreaseMoves();

            int points = CalculatePoints(matches, currentData);
            GameData.CurrentPoints += points;
            UpdateScoreForType(currentData.Type, points);

            MatchDestroy(matches);

            if (fromPlayer)
                HandleCombo();

            _gameUI.UpdateUI();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Decrements the player's remaining moves by one.
    /// </summary>
    private void DecreaseMoves()
    {
        GameData.CurrentMoves = Mathf.Max(0, GameData.CurrentMoves - 1);
    }

    /// <summary>
    /// Adds the scored points to the correct medicine type score bucket.
    /// </summary>
    private void UpdateScoreForType(MedicineType type, int points)
    {
        int index = (int)type;
        if (index >= 0 && index < GameData.ScorePerType.Length)
            GameData.ScorePerType[index] += points;
    }

    /// <summary>
    /// Increments the combo count if a combo is already active, otherwise just starts the timer.
    /// Updates GameData and restarts the combo timer.
    /// </summary>
    private void HandleCombo()
    {
        if (_comboTimerCoroutine != null)
        {
            _matchComboCount++;
            GameData.CurrentComboCount = _matchComboCount;
            StopCoroutine(_comboTimerCoroutine);
        }

        GameData.IsComboActive = true;
        _comboTimerCoroutine = StartCoroutine(ComboTimerRoutine());
    }

    /// <summary>
    /// Waits for the combo window to expire and then resets the combo count.
    /// </summary>
    private IEnumerator ComboTimerRoutine()
    {
        yield return new WaitForSeconds(_levelData.ComboWindow);
        ResetCombo();
    }

    /// <summary>
    /// Resets the combo count back to 1 and updates GameData.
    /// </summary>
    private void ResetCombo()
    {
        _matchComboCount = 1;
        GameData.CurrentComboCount = _matchComboCount;
        GameData.IsComboActive = false;
        _comboTimerCoroutine = null;
    }

    /// <summary>
    /// Calculates the points for a match, multiplied by the current combo count.
    /// </summary>
    private int CalculatePoints(HashSet<MedicineData> matches, MedicineData target)
    {
        return target.Points * matches.Count * _matchComboCount;
    }

    /// <summary>
    /// Collects all tiles in a line (horizontal or vertical) that match the target type.
    /// </summary>
    private HashSet<MedicineData> GetLineMatches(MedicineData origin, MedicineType targetType, bool horizontal)
    {
        HashSet<MedicineData> matches = new HashSet<MedicineData>();
        Stack<MedicineData> toCheck = new Stack<MedicineData>();

        matches.Add(origin);
        toCheck.Push(origin);

        while (toCheck.Count > 0)
        {
            MedicineData target = toCheck.Pop();

            Vector2Int pos = target.GetComponent<MedicineSelect>().Position;

            Vector2Int[] directions = horizontal
                ? new[] { new Vector2Int(pos.x + 1, pos.y), new Vector2Int(pos.x - 1, pos.y) }
                : new[] { new Vector2Int(pos.x, pos.y + 1), new Vector2Int(pos.x, pos.y - 1) };

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

    /// <summary>
    /// Gets all valid neighbours of the given tile.
    /// </summary>
    private List<MedicineData> GetNeighbours(Transform current)
    {
        Vector2Int pos = current.GetComponent<MedicineSelect>().Position;

        if (!IsValid(pos.x, pos.y)) return null;

        List<MedicineData> collectedNeighbors = new List<MedicineData>();

        Vector2Int[] directions = {
            new Vector2Int(pos.x,     pos.y + 1),
            new Vector2Int(pos.x,     pos.y - 1),
            new Vector2Int(pos.x - 1, pos.y),
            new Vector2Int(pos.x + 1, pos.y),
        };

        foreach (Vector2Int dir in directions)
        {
            if (IsValid(dir.x, dir.y))
                TryAddNeighbour(dir.x, dir.y, collectedNeighbors);
        }

        return collectedNeighbors;
    }

    /// <summary>
    /// Tries to add a neighbour tile to the list.
    /// </summary>
    private void TryAddNeighbour(int x, int y, List<MedicineData> neighbours)
    {
        MedicineData neighbour = _gridGeneration.Grid[x, y].GetComponent<MedicineData>();
        neighbours.Add(neighbour);
    }

    /// <summary>
    /// Destroys all tiles in the match and triggers a cascade.
    /// </summary>
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

    /// <summary>
    /// Checks whether the given grid coordinates are within bounds.
    /// </summary>
    private bool IsValid(int column, int row)
    {
        return column >= 0 && column < _gridGeneration.Width && row >= 0 && row < _gridGeneration.Height;
    }
}