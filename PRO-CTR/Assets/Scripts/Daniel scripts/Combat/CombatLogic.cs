using System;
using System.Collections;
using Daniel_scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class CombatLogic : MonoBehaviour
{
    [Header("Combat Actors")] [SerializeField]
    private PlayerCombat player;

    [SerializeField] private EnemyCombat enemy;

    [Header("UI Elements")] [SerializeField]
    private GameObject playerActionUI;

    [SerializeField] private GameObject enemyTurnUI;
    [SerializeField] private TextMeshProUGUI turnIndicatorText;

    [Header("Player Action Buttons")] [SerializeField]
    private GameObject fightButton;

    [SerializeField] private GameObject criticalButton;
    [SerializeField] private GameObject itemButton;
    
    [Header("Critical Button Images")]
    [SerializeField] private Sprite chargeSprite;
    [SerializeField] private Sprite criticalSprite;

    [SerializeField] private GameObject combatActionUI;
    [SerializeField] private PlayerLogic playerLogic;
    [SerializeField] private EnemyLogic enemyLogic;
    private enum TurnState
    {
        PlayerTurn,
        EnemyTurn,
        GameOver
    }

    private TurnState currentState;

    private bool isCriticalCharged = false;

    void Start()
    {
        // Check who initiated combat from the overworld
        if (TurnManager.combatInitiator != null)
        {
            bool playerInitiated = TurnManager.combatInitiator.CompareTag("Player");

            if (playerInitiated)
            {
                StartPlayerTurn();
            }
            else
            {
                StartEnemyTurn();
            }
        }
        else
        {
            // Default to player turn if no initiator data
            StartPlayerTurn();
        }
    }

    #region Player Turn

    void StartPlayerTurn()
    {
        currentState = TurnState.PlayerTurn;
        turnIndicatorText.text = "Your Turn";

        playerActionUI.SetActive(true);
        enemyTurnUI.SetActive(false);

        // Enable action buttons
        SetupPlayerActions();
    }

    void SetupPlayerActions()
    {
        // Clear previous listeners
        fightButton.GetComponent<Button>().onClick.RemoveAllListeners();
        criticalButton.GetComponent<Button>().onClick.RemoveAllListeners();
        itemButton.GetComponent<Button>().onClick.RemoveAllListeners();

        // Add new listeners
        fightButton.GetComponent<Button>().onClick.AddListener(OnFightSelected);
        criticalButton.GetComponent<Button>().onClick.AddListener(OnCriticalSelected);
        itemButton.GetComponent<Button>().onClick.AddListener(OnItemSelected);

        // Update critical button text based on charge state
        UpdateCriticalButton();
    }

    void UpdateCriticalButton()
    {
        UnityEngine.UI.Image buttonImage = criticalButton.GetComponent<UnityEngine.UI.Image>();
        if (buttonImage != null)
        {
            buttonImage.sprite = isCriticalCharged ? criticalSprite : chargeSprite;
        }
    }

    public void OnFightSelected()
    {
        Debug.Log("Player chose FIGHT");

        // Start fight minigame (timing bar, button mash, etc.)
        player.StartFightMinigame(() =>
        {
            playerActionUI.SetActive(false);
            
            float damage = player.CalculateFightDamage();
            enemy.TakeDamage(damage);

            if (enemy.IsDead())
            {
                EndCombat(true);
            }
            else
            {
                StartEnemyTurn();
            }
        });
    }

    public void OnCriticalSelected()
    {
        playerActionUI.SetActive(false);

        if (isCriticalCharged)
        {
            // Critical is charged - unleash powerful attack!
            Debug.Log("Player unleashed CRITICAL attack!");

            player.StartCriticalMinigame(() =>
            {
                float damage = player.CalculateCriticalDamage();
                enemy.TakeDamage(damage);

                // Reset charge after use
                isCriticalCharged = false;

                if (enemy.IsDead())
                {
                    EndCombat(true);
                }
                else
                {
                    StartEnemyTurn();
                }
            });
        }
        else
        {
            // Not charged yet - charge it up (skip turn)
            Debug.Log("Player is charging critical attack...");
            isCriticalCharged = true;

            // Show charge animation/effect
            player.ShowCriticalCharge();

            // Skip to enemy turn
            StartCoroutine(DelayedEnemyTurn());
        }
    }

    IEnumerator DelayedEnemyTurn()
    {
        // Give time for charge animation
        yield return new WaitForSeconds(1f);
        StartEnemyTurn();
    }

    public void OnItemSelected()
    {
        Debug.Log("Player chose ITEM");
        playerActionUI.SetActive(false);

        // Show item inventory
        player.ShowItemMenu(() =>
        {
            // After item use
            StartEnemyTurn();
        });
    }

    #endregion

    #region Enemy Turn

    void StartEnemyTurn()
    {
        currentState = TurnState.EnemyTurn;
        turnIndicatorText.text = "Enemy Turn";

        playerActionUI.SetActive(false);
        enemyTurnUI.SetActive(true);

        // Enemy performs action then starts minigame
        StartCoroutine(EnemyTurnSequence());
    }

    IEnumerator EnemyTurnSequence()
    {
        // Enemy dialogue or action animation
        yield return new WaitForSeconds(1f);

        // Start enemy attack minigame (dodge bullets, etc.)
        enemy.StartAttackMinigame(() =>
        {
            int damage = enemy.CalculateAttackDamage();
            player.TakeDamage(damage);

            if (player.IsDead())
            {
                EndCombat(false);
            }
            else
            {
                StartPlayerTurn();
            }
        });
    }

    #endregion

    #region Combat End

    void EndCombat(bool playerWon)
    {
        currentState = TurnState.GameOver;

        if (playerWon)
        {
            Debug.Log("Player won!");
            combatActionUI.SetActive(false);
            // Show victory screen, give rewards, return to overworld
        }
        else
        {
            Debug.Log("Player lost!");
            // Show game over screen
        }
    }

    #endregion
}


// public float waitTime = 1;
// private bool done = false;
// [SerializeField] private Skillcheck skillcheckGameObject;
// [SerializeField] private EnemyLogic enemy;
//
// private void Start()
// {
//     TurnActor initiator = TurnManager.combatInitiator;
// }
//
// public void OnAttackButton()
// {
//     StartCoroutine(Delay(waitTime));
// }
//
// IEnumerator Delay(float time)
// {
//     yield return new WaitForSeconds(time);
//     skillcheckGameObject.StartSkillCheck();
// }