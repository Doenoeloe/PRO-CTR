using UnityEngine;

public class PlayerMovementMinigame : MonoBehaviour
{
    Vector3 MousePos;
    public bool isAlive = true;
    float healthPoints = 100.0f;
    public bool canMove;
    float moveSpeed = 100.0f;
    Rigidbody2D rb;
    void OnEnable()
    {
        // Get the component only after this GameObject (and its parents) become active
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Rigidbody2D found: {rb != null}");
        Cursor.visible = false;
    }

    // Update is called once per physics frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        if (!canMove || rb == null) {
            return;
        }
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (MousePos - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints < 80)
        {
            isAlive = false;
            canMove = false;
        }
    }

    public float DisplayHealthInfo()
    {
        return healthPoints;
    }
}
