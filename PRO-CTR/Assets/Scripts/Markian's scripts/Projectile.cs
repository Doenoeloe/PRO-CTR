using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 6f;
    public float knockbackForce = 13f;
    Vector3 direction;
    float damage = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Damage");
            collision.GetComponent<PlayerMovementMinigame>().TakeDamage(damage);
            collision.GetComponent<PlayerMovementMinigame>().ApplyKnockBack(knockbackForce, -direction);
            Debug.Log($"Player Health: {collision.GetComponent<PlayerMovementMinigame>().DisplayHealthInfo()};");
        }
    }
    public void Init(Vector3 pDir)
    {
        direction = pDir;
    }
    private void Update()
    {
        transform.position += (direction * speed * Time.deltaTime);
        Debug.Log(direction);
    }
}
