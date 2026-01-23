using System.Collections;
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
            collision.GetComponent<PlayerMovementMinigame>().TakeDamage(damage);
        }
    }
    public void Init(Vector3 pDir)
    {
        direction = pDir;
        StartCoroutine(WaitTillDestroy());
    }
    private void Update()
    {
        transform.position += (direction * speed * Time.deltaTime);
    }
    IEnumerator WaitTillDestroy()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
