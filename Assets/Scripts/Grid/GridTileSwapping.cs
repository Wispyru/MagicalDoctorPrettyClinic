using UnityEngine;

public class GridTileSwapping : MonoBehaviour
{
    private GridGeneration _gridGeneration;
    private DisplayLevelData _displayLevelData;

    private void Start()
    {
        _gridGeneration = FindAnyObjectByType<GridGeneration>();
        _displayLevelData = FindAnyObjectByType<DisplayLevelData>();
    }

    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        GameObject tile1 = _gridGeneration.Grid[tile1Position.x, tile1Position.y];
        GameObject tile2 = _gridGeneration.Grid[tile2Position.x, tile2Position.y];

        PerformSwap(tile1, tile2, tile1Position, tile2Position);

        bool tile1Matched = _gridGeneration.Matching.CheckForMatches(tile1, fromPlayer: true);
        bool tile2Matched = _gridGeneration.Matching.CheckForMatches(tile2, fromPlayer: true);

        if (!tile1Matched && !tile2Matched)
        {
            PerformSwap(tile1, tile2, tile2Position, tile1Position);
        }



        tile1.GetComponent<MedicineDrag>().ResetToCurrentPosition();
        tile2.GetComponent<MedicineDrag>().ResetToCurrentPosition();
    }

    private void PerformSwap(GameObject tile1, GameObject tile2, Vector2Int tile1Position, Vector2Int tile2Position)
    {
        tile1.transform.position = _gridGeneration.GetWorldPosition(tile2Position.x, tile2Position.y);
        tile2.transform.position = _gridGeneration.GetWorldPosition(tile1Position.x, tile1Position.y);

        _gridGeneration.Grid[tile1Position.x, tile1Position.y] = tile2;
        _gridGeneration.Grid[tile2Position.x, tile2Position.y] = tile1;

        tile1.GetComponent<MedicineSelect>().Position = tile2Position;
        tile2.GetComponent<MedicineSelect>().Position = tile1Position;

        if (GameData.CurrentMoves != 0 && GameData.CurrentRound >= 0)
        {
            GameData.CurrentMoves--;
            _displayLevelData.UpdateUIText();
        }
    }
}