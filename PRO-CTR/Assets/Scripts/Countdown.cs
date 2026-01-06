using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    TextMeshProUGUI countdownText;
    GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        countdownText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartCountDown());
    }
    IEnumerator StartCountDown()
    {
        for (int i = 3; i >= 0; i--)
        {
            if (i == 0)
            {
                countdownText.gameObject.SetActive(false);
            }
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
