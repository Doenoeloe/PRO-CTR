using System.Reflection;
using UnityEngine;

public class MinigameInput : MonoBehaviour
{
    [SerializeField] GameObject minigame;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.M)) return;

        if (minigame == null)
        {
            Debug.LogError("MinigameInput: 'minigame' GameObject is not assigned in the Inspector.");
            return;
        }
        else
        {
            minigame.SetActive(true);
            minigame.GetComponentInChildren<MinigameStateManager>().StartMinigame();
        }
    }
}
