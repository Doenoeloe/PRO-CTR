using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelection : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap obstacleTilemap;
    [SerializeField] private float offset = 0.5f;
    [SerializeField] private Vector2 gridSize = new Vector2(1f, 1f);

    private Vector2Int highlightedTilePosition = Vector2Int.zero;

    private void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int gridPos = new Vector2Int(
            Mathf.FloorToInt(mouseWorldPos.x / gridSize.x) * Mathf.RoundToInt(gridSize.x),
            Mathf.FloorToInt(mouseWorldPos.y / gridSize.y) * Mathf.RoundToInt(gridSize.y)
            );

        bool isObstacleTile = false;
        if (obstacleTilemap != null)
        {
            Vector3Int cellPos = obstacleTilemap.WorldToCell(mouseWorldPos);
            if (obstacleTilemap.HasTile(cellPos) && obstacleTilemap.GetTile(cellPos) != null)
            {
                isObstacleTile = true;

            }

            if (!isObstacleTile)
            {
                highlightedTilePosition = gridPos;
                Vector2 worldPos = Gridtiles.GridtoWorld(gridPos) + new Vector2(offset, offset);
                transform.position = worldPos;
            }
        }
    }


    public Vector2Int HighlightedTilePosition
    {
        get { return highlightedTilePosition; }
    }

    public bool IsHighlightedTileClicked(Vector2 clickedPosition)
    {
        Vector2Int gridPos = Gridtiles.WorldtoGrid(clickedPosition);
        return gridPos == highlightedTilePosition;
    }

    public Vector2 GetHighlightedTilePosition()
    {
        return Gridtiles.GridtoWorld(highlightedTilePosition);
    }

    public bool IsTileObstacle(Vector2Int position)
    {
        Vector3 worldPosition = Gridtiles.GridtoWorld(position); 
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if(hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
