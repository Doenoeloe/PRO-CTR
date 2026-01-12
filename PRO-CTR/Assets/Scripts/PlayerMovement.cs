using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool isRepeatedMovement = false;
    [SerializeField] private float moveDuration = 0.1f;

    [Header("Tilemap")]
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;

    private bool isMoving = false;

    bool up, down, right, left;

    private void Awake()
    {
        if (collisionTilemap == null)
        {
            collisionTilemap = GameObject.Find("CollisionTilemap")?.GetComponent<Tilemap>();
        }

        if (grid == null)
        {
            grid = FindObjectOfType<Grid>();
        }
    }

    private void Start()
    {
        Vector3Int cell = grid.WorldToCell(transform.position);
        transform.position = grid.GetCellCenterWorld(cell);
    }

    void Update()
    {
        if (isRepeatedMovement)
        {
            up = Input.GetKey(KeyCode.UpArrow);
            down = Input.GetKey(KeyCode.DownArrow);
            left = Input.GetKey(KeyCode.LeftArrow);
            right = Input.GetKey(KeyCode.RightArrow);
        }
        else
        {
            up = Input.GetKey(KeyCode.UpArrow);
            down = Input.GetKey(KeyCode.DownArrow);
            left = Input.GetKey(KeyCode.LeftArrow);
            right = Input.GetKey(KeyCode.RightArrow);

        }

        Vector2Int direction = Vector2Int.zero;

        if (up)
        {
            direction = Vector2Int.up;
        }
        else if (down)
        {
            direction = Vector2Int.down;
        }
        else if (left)
        {
            direction = Vector2Int.left;
        }
        else if (right)
        {
            direction = Vector2Int.right;
        }

        if (direction != Vector2Int.zero)
        {
            TryMove(direction);
        }


    }


    private void TryMove(Vector2Int direction)
    {
        Vector3Int currentCell = grid.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + new Vector3Int(direction.x, direction.y, 0);

        if (!groundTilemap != null && collisionTilemap.HasTile(targetCell))
        {
            return;
        }

        if (collisionTilemap != null && collisionTilemap.HasTile(targetCell))
        {
            return;
        }

        Vector3 targetWorldPosition = grid.GetCellCenterWorld(targetCell);
        StartCoroutine(Move(targetWorldPosition));


    }
    private IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;
         
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}
