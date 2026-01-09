using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 6f;
    Vector3 direction;
    float damage = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Damage");
            collision.GetComponent<PlayerMovement>().TakeDamage(damage);
            Debug.Log($"Player Health: {collision.GetComponent<PlayerMovement>().DisplayHealthInfo()};");
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
