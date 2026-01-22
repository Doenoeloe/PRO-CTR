using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//actually Spawner
public class WaveLogic : MonoBehaviour
{
    //Projectile prefab
    [SerializeField] GameObject Projectile_v1;
    //UI Text for wave info
    [SerializeField] TextMeshProUGUI WaveText;
    //Reference to player
    GameObject player;
    //radius of circle spawn
    float circleRadius = 5.0f;
    //number of projectiles in circle
    int circleProjectilesAmmount = 16;
    //spawn radius from player
    float spawnRadius = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //Spawns a wall of projectiles aimed at the player
    public void SpawnProjectileWall()
    {
        //space between projectiles
        float spacing = 0.5f;

        Vector3 centerPos = player.transform.position + (Vector3)Random.insideUnitCircle.normalized * spawnRadius;

        Vector3 directionToPlayer = (player.transform.position - centerPos).normalized;
        Vector3 perpendicular = Vector3.Cross(directionToPlayer, Vector3.forward);

        for (int i = 0; i <= 4; i++)
        {
            Vector3 spawnPos = centerPos + perpendicular * i * spacing;

            GameObject bullet = Instantiate(Projectile_v1, spawnPos, Quaternion.identity);
            bullet.GetComponent<Projectile>().Init(directionToPlayer);
        }
    }
    //Spawns a circle of projectiles around player aimed outwards
    public IEnumerator SpawnCircle()
    {
        Vector3 circleCentrum = player.transform.position;
        for (int i = 0; i < circleProjectilesAmmount; i++)
        {
            float angle = i * (360f / 16);
            Vector2 dir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad));
            Vector3 spawnPos = circleCentrum + (Vector3)dir * circleRadius;
            GameObject bullet = Instantiate(Projectile_v1, spawnPos, Quaternion.identity);
            yield return null;
            bullet.GetComponent<Projectile>().Init(-dir);
        }
    }
    //Spawns a projectile at random position around player aimed at player
    public void RandomBulletSpawner()
    {
        Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = player.transform.position + randomOffset;

        GameObject bullet = Instantiate(Projectile_v1, spawnPos, Quaternion.identity);

        Vector2 direction = (player.transform.position - spawnPos).normalized;
        bullet.GetComponent<Projectile>().Init(direction);
    }
    //Timer for game over after defending for certain time
    public IEnumerator DisplayEndGameMessage(float pSeconds)
    {
        yield return new WaitForSecondsRealtime(pSeconds);
        WaveText.gameObject.SetActive(true);
        if (player.GetComponent<PlayerMovementMinigame>().isAlive)
        {
            WaveText.text = "Nice! You have defended yourself.";
        }
        else
        {
            WaveText.text = "Loser";
        }
        yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public IEnumerator StartGameCountDown(int pSeconds)
    {
        Debug.Log(WaveText != null);
        WaveText.gameObject.SetActive(true);
        for (int i = pSeconds; i >= 0; i--)
        {
            if (i == 0)
            {
                WaveText.gameObject.SetActive(false);
                player.GetComponent<PlayerMovementMinigame>().canMove = true;
            }
            WaveText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

}
