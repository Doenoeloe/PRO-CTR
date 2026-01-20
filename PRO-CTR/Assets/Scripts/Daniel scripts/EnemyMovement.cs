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
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        obstacleTilemap = FindObjectOfType<ObstacleTilemap>();
    }

    private void TryMoveTowardsPlayer()
    {
        Vector2 startPos = Gridtiles.GridtoWorld(
            Gridtiles.WorldtoGrid(transform.position));

        Vector2 targetPos = Gridtiles.GridtoWorld(
            Gridtiles.WorldtoGrid(player.position));

        List<Vector2> fullPath = AStar.FindPath(
            startPos,
            targetPos,
            gridSize,
            obstacleTilemap.IsTileObstacle
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

            while ((Vector2)transform.position != target)
            {
                float step = moveSpeed * Time.fixedDeltaTime;
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
