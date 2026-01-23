using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private Vector2 gridSize = new Vector2(1f, 1f);
    [SerializeField] private float repathInterval = 0.5f;

    private Transform player;
    private ObstacleTilemap obstacleTilemap;

    private bool isMoving;
    private Coroutine moveCoroutine;
    
    [SerializeField] private int maxTilesPerMove = 3;
    public Action OnMoveFinished;
    
    private EnemyManager enemyManager;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        player = playerMovement.transform;

        obstacleTilemap = FindObjectOfType<ObstacleTilemap>();
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    private void TryMoveTowardsPlayer()
    {
        Vector2Int enemyTile  = Gridtiles.WorldtoGrid(transform.position);
        Vector2Int playerTile = Gridtiles.WorldtoGrid(player.position);

        // Direction from enemy to player
        Vector2Int dir = playerTile - enemyTile;

        // Clamp to maximum of 1 tile per axis
        dir = new Vector2Int(
            Mathf.Clamp(dir.x, -1, 1),
            Mathf.Clamp(dir.y, -1, 1)
        );

        // Target tile is 1 step away from player
        Vector2Int targetTile = playerTile - dir;
        Vector2 targetPos = Gridtiles.GridtoWorld(targetTile);

        // Find path
        List<Vector2> fullPath = AStar.FindPath(
            Gridtiles.GridtoWorld(enemyTile),
            targetPos,
            gridSize,
            tile =>
            {
                Vector2Int gridTile = Gridtiles.WorldtoGrid(tile);

                // Block walls
                if (obstacleTilemap.IsTileObstacle(gridTile))
                    return true;

                // Block other enemies (but allow self)
                if (enemyManager.IsTileOccupied(gridTile) &&
                    gridTile != enemyTile)
                    return true;

                // Do NOT include player tile here because we stop adjacent
                return false;
            }
        );

        if (fullPath == null || fullPath.Count <= 1)
            return;

        int tilesToMove = Mathf.Min(maxTilesPerMove, fullPath.Count - 1);
        List<Vector2> limitedPath = new List<Vector2>();

        for (int i = 1; i <= tilesToMove; i++)
        {
            limitedPath.Add(fullPath[i]);
        }

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveAlongPath(limitedPath));
    }

    private IEnumerator MoveAlongPath(List<Vector2> path)
    {
        isMoving = true;
        for (int i = 0; i < path.Count; i++)
        {
            Vector2 target = path[i] + gridSize / 2f;

            while (Vector2.Distance(transform.position, target) > 0.01f)
            {
                float step = moveSpeed * Time.fixedDeltaTime;

                Vector2Int nextTile = Gridtiles.WorldtoGrid(target);
                Vector2Int currentTile = Gridtiles.WorldtoGrid(transform.position);

                if (
                    obstacleTilemap.IsTileObstacle(nextTile) ||
                    (enemyManager.IsTileOccupied(nextTile) &&
                     nextTile != currentTile)
                )
                {
                    isMoving = false;
                    yield break;
                }

                transform.position = Vector2.MoveTowards(
                    transform.position, target, step);

                yield return new WaitForFixedUpdate();
            }
        }

        isMoving = false;
        OnMoveFinished?.Invoke();
    }
    
    public void StartEnemyTurn()
    {
        TryMoveTowardsPlayer();
    }
}
