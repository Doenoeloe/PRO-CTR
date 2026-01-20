using System.Collections.Generic;
using Daniel_scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private List<TurnActor> actors;
    int actorIndex = 0;

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private int combatRange = 1; // Grid distance to trigger combat
    [SerializeField] private string combatSceneName = "CombatScene";
    
    public static TurnActor combatInitiator; // Who started the combat
    public static TurnActor combatTarget; // Who they're fighting

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
        
        // Check if the current actor is in range of another actor
        CheckCombatRange(actors[actorIndex]);
        
        actorIndex = (actorIndex + 1) % actors.Count;
        actors[actorIndex].StartTurn();
        UpdateUI();
    }

    void CheckCombatRange(TurnActor currentActor)
    {
        foreach (TurnActor otherActor in actors)
        {
            if (otherActor == currentActor) continue;
            
            // Grid-based distance (Manhattan distance)
            int distance = Mathf.Abs(Mathf.RoundToInt(currentActor.transform.position.x - otherActor.transform.position.x)) +
                          Mathf.Abs(Mathf.RoundToInt(currentActor.transform.position.z - otherActor.transform.position.z));
            
            if (distance <= combatRange)
            {
                InitiateCombat(currentActor, otherActor);
                return;
            }
        }
    }

    void InitiateCombat(TurnActor initiator, TurnActor target)
    {
        Debug.Log($"{initiator.name} initiated combat with {target.name}!");
        
        combatInitiator = initiator;
        combatTarget = target;
        
        // Load combat scene
        SceneManager.LoadScene(combatSceneName);
    }

    void UpdateUI()
    {
        textMesh.text = actors[actorIndex].name;
    }
}