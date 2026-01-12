using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private List<PlayerLogic> players;
    int playerIndex = 0;
    [SerializeField] TextMeshProUGUI textMesh;
    void Start()
    {
        foreach (PlayerLogic player in players)
        {
            player.enabled = false;
        } 
        Debug.Log(players.Count); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            players[playerIndex].enabled = false;

            playerIndex++;

            if (playerIndex >= players.Count)
            {
                playerIndex = 0;
            }

            players[playerIndex].enabled = true;
        }

        textMesh.text = players[playerIndex].gameObject.name;
    }

}
