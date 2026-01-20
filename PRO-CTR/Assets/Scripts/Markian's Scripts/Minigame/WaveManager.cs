using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//actually Spawner
public class WaveManager : MonoBehaviour
{
    //Projectile prefab
    [SerializeField] GameObject Projectile_v1;
    //UI Text for wave info
    [SerializeField] TextMeshProUGUI WaveText;
    GameObject player;
    //spawn radius from player
    float spawnRadius = 10.0f;
    //time between spawns
    float spawnPediod = 0.75f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Invoke  a timer for game over after 60 seconds
        StartCoroutine(GameOverTimer(60));
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(RandomBulletSpawner), 3.5f, spawnPediod);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //Check if player is alive
            if (!player.GetComponent<PlayerMovementMinigame>().isAlive)
            {
                player.GetComponent<PlayerMovementMinigame>().canMove = false;
                WaveText.gameObject.SetActive(true);
                WaveText.text = "Game Over";
                StartCoroutine(SceneChangeTimer(2));
                

            }
        }
        //Debug key to spawn projectile wall
        if (Input.GetKeyDown(KeyCode.F)){
            SpawnProjectileWall();
        }
        //Debug key to spawn circle of projectiles
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SpawnCircle(5, 11));
        }
    }
    //Spawns a wall of projectiles aimed at the player
    public void SpawnProjectileWall()
    {
        //space between projectiles
        float spacing = 0.5f;

        Vector3 centerPos = player.transform.position +
                            (Vector3)Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 directionToPlayer = (player.transform.position - centerPos).normalized;
        Vector3 perpendicular = Vector3.Cross(directionToPlayer, Vector3.forward);

        for (int i = 0; i <= 4; i++)
        {
            Vector3 spawnPos = centerPos + perpendicular * i * spacing;

            GameObject bullet = Instantiate(Projectile_v1, spawnPos, Quaternion.identity);
            bullet.GetComponent<Projectile>().Init(directionToPlayer);
        }
    }

    IEnumerator SpawnCircle(float pRadius, int pCount)
    {
        Vector3 circleCentrum = player.transform.position;
        for (int i = 0; i < pCount; i++)
        {
            float angle = i * (360f / 16);
            Vector2 dir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
            Vector3 spawnPos = circleCentrum + (Vector3)dir * pRadius;
            GameObject bullet = Instantiate(Projectile_v1, spawnPos, Quaternion.identity);
            yield return null;
            bullet.GetComponent<Projectile>().Init(-dir);
        }
    }
    void RandomBulletSpawner()
    {
        Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = player.transform.position + randomOffset;

        GameObject bullet = Instantiate(Projectile_v1, spawnPos, Quaternion.identity);

        Vector2 direction = (player.transform.position - spawnPos).normalized;
        bullet.GetComponent<Projectile>().Init(direction);
    }
    IEnumerator SceneChangeTimer(float pSeconds)
    {
        yield return new WaitForSecondsRealtime(pSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator GameOverTimer(float pSeconds)
    {
        yield return new WaitForSecondsRealtime(pSeconds);
        if (player.GetComponent<PlayerMovementMinigame>().isAlive)
        {
            WaveText.gameObject.SetActive(true);
            WaveText.text = "Nice! You have defended yourself.";
            yield return new WaitForSecondsRealtime(3);
            //Here you can add more code for what happens after winning(variable "hasDefended" and switch to another scene);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
