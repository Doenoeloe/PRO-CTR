using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private HashSet<Vector2Int> occupiedTiles = new HashSet<Vector2Int>();

    public bool IsTileOccupied(Vector2Int tile)
    {
        return occupiedTiles.Contains(tile);
    }

    public void RegisterOccupant(GridOccupant occupant)
    {
        occupiedTiles.Add(occupant.GridPosition);
    }

    public void UnregisterOccupant(GridOccupant occupant)
    {
        occupiedTiles.Remove(occupant.GridPosition);
    }

    public void UpdateOccupantPosition(Vector2Int oldPos, Vector2Int newPos)
    {
        occupiedTiles.Remove(oldPos);
        occupiedTiles.Add(newPos);
    }
}