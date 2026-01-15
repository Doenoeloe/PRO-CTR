using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleTilemap : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private HashSet<Vector3Int> obstacleTilePositions = new HashSet<Vector3Int>();

    private void Awake()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }

        InitializeObstacleTiles();
    }

    private void InitializeObstacleTiles()
    {
        obstacleTilePositions.Clear();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector3Int tilePosition = new Vector3Int(bounds.x + x, bounds.y + y, 0);
                    obstacleTilePositions.Add(tilePosition);
                }
            }
        }
    }
    public bool IsTileObstacle(Vector2 position)
    {
        Vector3Int gridPos = tilemap.WorldToCell(position);
        return obstacleTilePositions.Contains(gridPos);
    }
}
