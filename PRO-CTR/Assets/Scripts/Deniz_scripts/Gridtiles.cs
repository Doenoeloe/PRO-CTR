using UnityEngine;

public class Gridtiles : MonoBehaviour
{
    public static Vector2Int WorldtoGrid(Vector2 worldPos)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPos.x), Mathf.FloorToInt(worldPos.y));
    }

    public static Vector2 GridtoWorld(Vector2Int gridPos)
    {
        return new Vector2(gridPos.x, gridPos.y);
    }


}
