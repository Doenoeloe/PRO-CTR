using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameObject Projectile_v1;
    [SerializeField] TextMeshProUGUI WaveText;
    GameObject player;

    float spawnRadius = 10.0f;
    float spawnPediod = 0.75f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(RandomBulletSpawner), 3.5f, spawnPediod);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (!player.GetComponent<PlayerMovement>().isAlive)
            {
                player.GetComponent<PlayerMovement>().canMove = false;
                WaveText.gameObject.SetActive(true);
                WaveText.text = "Game Over";
                StartCoroutine(Timer(2));
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                

            }
        }
    }
    void SpawnProjectile(GameObject pProjectile, Vector2 pPos)
    {
        GameObject Projectile = Instantiate(pProjectile, pPos, Quaternion.identity);

    }
    IEnumerator SpawnCircle(GameObject pProjectile, Vector2 pPlayerPos, int pCount)
    {
        for (int i = 0; i < pCount; i++)
        {
            float angle = i * (360f / 16);
            Vector2 dir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
        }
        yield return null;
    }
    void RandomBulletSpawner()
    {
        Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = player.transform.position + randomOffset;

        GameObject bullet = Instantiate(Projectile_v1, spawnPos, Quaternion.identity);

        Vector2 direction = (player.transform.position - spawnPos).normalized;
        bullet.GetComponent<Projectile>().Init(direction);
    }
    IEnumerator Timer(float pSeconds)
    {
        yield return new WaitForSecondsRealtime(pSeconds);
    }
}
