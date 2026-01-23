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

    [Header("Critical Button Images")] [SerializeField]
    private Sprite chargeSprite;

    [SerializeField] private Sprite criticalSprite;

    [SerializeField] private GameObject combatActionUI;
    [SerializeField] private PlayerLogic playerLogic;
    [SerializeField] private EnemyLogic enemyLogic;

    [SerializeField] private Animator animator;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject frames;
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

        animator.SetTrigger("Idle");
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
            float damage = player.CalculateFightDamage();
            enemy.TakeDamage(damage);

            if (enemy.IsDead())
            {
                EndCombat(true);
            }
            else
            {
                StartCoroutine(DelayedEnemyTurn());
            }
        });
    }

    public void OnCriticalSelected()
    {
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
                    StartCoroutine(DelayedEnemyTurn());
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
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        playerActionUI.SetActive(false);
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
        PlaceFramesInWorld();
        yield return new WaitForSeconds(1f);
        // Start enemy attack minigame (dodge bullets, etc.)
        enemy.StartAttackMinigame(() =>
        {
            
            int damage = enemy.CalculateAttackDamage();
            player.TakeDamage(damage);

            if (player.IsDead())
            {
                EndCombat(false);
                mainCamera.orthographicSize = 2.8f;
            }
            else
            {
                StartPlayerTurn();
                mainCamera.orthographicSize = 2.8f;
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
            Destroy(enemy.gameObject);
            // Show victory screen, give rewards, return to overworld
        }
        else
        {
            Debug.Log("Player lost!");
            // Show game over screen
        }
    }

    #endregion
    
    void PlaceFramesInWorld()
    {
        mainCamera.orthographicSize = 5f;
        
        if (frames == null || mainCamera == null) return;

        // Vind frames
        Transform leftFrame = frames.transform.Find("LeftFrame");
        Transform rightFrame = frames.transform.Find("RightFrame");
        Transform topFrame = frames.transform.Find("TopFrame");
        Transform bottomFrame = frames.transform.Find("BottomFrame");

        // Zorg dat we een Z-waarde gebruiken die op het zichtbare vlak ligt
        float z = Mathf.Abs(mainCamera.transform.position.z); // voor orthographic/perspective

        // Linker en rechter rand
        leftFrame.position = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, z));
        rightFrame.position = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, z));

        // Boven en onder rand
        topFrame.position = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, z));
        bottomFrame.position = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, z));
    }


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