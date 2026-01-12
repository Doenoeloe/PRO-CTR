using System.Collections;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private float gridSize = 1f;

    private Vector2 targetGridPosition;
    private Coroutine moveRoutine;

    private void Start()
    {
        SnapToGrid();
    }

    private void Update()//update methodeee
    {
        if (Input.GetMouseButtonDown(0) && moveRoutine == null)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;


            float targetX = Mathf.Round(mouseWorld.x / gridSize) * gridSize;
            float targetY = Mathf.Round(mouseWorld.y / gridSize) * gridSize;

            targetGridPosition = new Vector2(targetX, targetY);

            moveRoutine = StartCoroutine(MoveToTarget());
        }
    }

    private IEnumerator MoveToTarget()
    {
        while ((Vector2)transform.position != targetGridPosition)
        {
            Vector2 direction = (targetGridPosition - (Vector2)transform.position).normalized;

            if (Mathf.Abs(targetGridPosition.x - transform.position.x) > Mathf.Abs(targetGridPosition.y - transform.position.y))
            {
                direction = new Vector2(Mathf.Sign(targetGridPosition.x - transform.position.x), 0);
            }

            else
            {
                direction = new Vector2(0, Mathf.Sign(targetGridPosition.y - transform.position.y));
            }


            Vector2 start = transform.position;
            Vector2 end = start + direction * gridSize;
            float elapsed = 0f;

            while (elapsed < moveDuration)
            {
                elapsed += Time.deltaTime;
                transform.position = Vector2.Lerp(start, end, elapsed / moveDuration);
                yield return null;
            }

            transform.position = end;
            SnapToGrid();
        }

        moveRoutine = null;
    }

    private void SnapToGrid()
    {
        float afgerondX = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float afgerondY = Mathf.Round(transform.position.y / gridSize) * gridSize;

        transform.position = new Vector2(afgerondX, afgerondY);
    }
}
