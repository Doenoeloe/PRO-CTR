using UnityEngine;

public class PlayerMovementMinigame : MonoBehaviour
{
    Vector3 MousePos;
    public bool isAlive = true;
    float healthPoints = 100.0f;
    public bool canMove;
    float moveSpeed = 100.0f;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            MovePlayer();
        }
    }
    void MovePlayer()
    {
        if (!canMove) {
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
