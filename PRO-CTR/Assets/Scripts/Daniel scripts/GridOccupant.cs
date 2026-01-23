using UnityEngine;

public class GridOccupant : MonoBehaviour
{
    private EnemyManager enemyManager;
    private Vector2Int lastGridPos;

    public Vector2Int GridPosition =>
        Gridtiles.WorldtoGrid(transform.position);

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    private void Start()
    {
        lastGridPos = GridPosition;
        enemyManager.RegisterOccupant(this);
    }

    private void Update()
    {
        Vector2Int current = GridPosition;

        if (current != lastGridPos)
        {
            enemyManager.UpdateOccupantPosition(lastGridPos, current);
            lastGridPos = current;
        }
    }

    private void OnDestroy()
    {
        enemyManager.UnregisterOccupant(this);
    }
}
