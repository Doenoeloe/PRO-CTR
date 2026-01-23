using System;
using System.Collections;
using System.Collections.Generic;
using Daniel_scripts;
using UnityEngine;

public enum MovementDirection
{
    Up,
    Down,
    Left,
    Right
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 gridSize = new Vector2(1f, 1f);

    [SerializeField] private ObstacleTilemap obstacleTilemap;
    [SerializeField] private ObstacleTilemap petOnlyWall;
    [SerializeField] private TileSelection tileSelection;

    private Vector2 targetPosition;
    private bool isMoving = false;
    private MovementDirection currentDirection = MovementDirection.Down;

    public bool CanMove { get; set; }
    public Action OnMoveFinished;

    [SerializeField] private Animator animator;
    [SerializeField] private EnemyLogic enemyLogic;
    [SerializeField] private EnemyManager enemyManager;
    private Vector2Int CurrentGridTile =>
        Gridtiles.WorldtoGrid(transform.position);

    private void Awake()
    {
        if (obstacleTilemap == null)
        {
            obstacleTilemap = FindObjectOfType<ObstacleTilemap>();
        }

        if (petOnlyWall == null)
        {
            petOnlyWall = FindObjectOfType<ObstacleTilemap>();
        }

        if (tileSelection == null)
        {
            tileSelection = FindObjectOfType<TileSelection>();
        }


        if (obstacleTilemap == null || petOnlyWall == null || tileSelection == null)
        {
            enabled = false;
            return;
        }

        if (enemyManager == null)
        {
            enemyManager = FindObjectOfType<EnemyManager>();
        }
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        if (!CanMove) return;

        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            targetPosition = tileSelection.GetHighlightedTilePosition();
            Vector2Int clickedTile = Gridtiles.WorldtoGrid(targetPosition);

            bool isBlocked =
                obstacleTilemap.IsTileObstacle(clickedTile) ||
                petOnlyWall.IsTileObstacle(clickedTile) ||
                (enemyManager.IsTileOccupied(clickedTile) &&
                 clickedTile != CurrentGridTile);

            if (!isBlocked && targetPosition != Vector2.zero)
            {
                FindPathToTargetPosition();
            }
        }

        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void FindPathToTargetPosition()
    {
        Vector2 startPosition = Gridtiles.GridtoWorld(Gridtiles.WorldtoGrid(transform.position));

        List<Vector2> path = AStar.FindPath(
            startPosition,
            targetPosition,
            gridSize,
            tile =>
            {
                Vector2Int gridTile = Gridtiles.WorldtoGrid(tile);

                if (obstacleTilemap.IsTileObstacle(gridTile))
                    return true;

                if (petOnlyWall.IsTileObstacle(gridTile))
                    return true;

                // Block other occupants, but NOT yourself
                if (enemyManager.IsTileOccupied(gridTile) &&
                    gridTile != CurrentGridTile)
                    return true;

                return false;
            }
        );

        if (path != null && path.Count > 0)
        {
            StartCoroutine(MoveAlongPath(path));
        }
    }

    private IEnumerator MoveAlongPath(List<Vector2> path)
    {
        isMoving = true;
        animator.SetBool("IsWalking", true);
        int currentWaypointIndex = 0;
        Vector2 previousPosition = transform.position;

        while (currentWaypointIndex < path.Count)
        {
            targetPosition = path[currentWaypointIndex] + gridSize / 2f;

            while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
            {
                Vector2Int nextTile = Gridtiles.WorldtoGrid(targetPosition);

                if (
                    obstacleTilemap.IsTileObstacle(nextTile) ||
                    petOnlyWall.IsTileObstacle(nextTile) ||
                    (enemyManager.IsTileOccupied(nextTile) &&
                     nextTile != CurrentGridTile)
                )
                {
                    transform.position = previousPosition;
                    isMoving = false;
                    yield break;
                }

                previousPosition = transform.position;
                float step = moveSpeed * Time.fixedDeltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                yield return new WaitForFixedUpdate();
            }

            transform.position = targetPosition;
            currentWaypointIndex++;
        }

        isMoving = false;
        animator.SetBool("IsWalking", false);
        OnMoveFinished?.Invoke();
    }


    private void MoveTowardsTarget()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                currentDirection = MovementDirection.Right;
            }
            else
            {
                currentDirection = MovementDirection.Left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                currentDirection = MovementDirection.Up;
            }
            else
            {
                currentDirection = MovementDirection.Down;
            }
        }
    }
}