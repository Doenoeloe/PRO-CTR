using System.Collections.Generic;
using Daniel_scripts;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    
    [SerializeField] private List<TurnActor> actors;
    int actorIndex = 0;

    [SerializeField] private TextMeshProUGUI textMesh;

    void Start()
    {
        foreach (TurnActor actor in actors)
        {
            actor.enabled = false;
            actor.OnTurnFinished += AdvanceTurn;
        }

        actors[actorIndex].StartTurn();
        UpdateUI();
    }

    void AdvanceTurn()
    {
        Debug.Log(actors);
        actorIndex = (actorIndex + 1) % actors.Count;
        actors[actorIndex].StartTurn();
        UpdateUI();
    }

    void UpdateUI()
    {
        textMesh.text = actors[actorIndex].name;
    }
}
