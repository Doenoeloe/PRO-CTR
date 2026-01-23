using System.Collections;
using UnityEngine;
public class MinigameStateManager : MonoBehaviour
{
    [SerializeField] GameObject minigame;
    GameObject player;
    WaveState CurrentState;
    [SerializeField] WaveLogic waveManager;
    public WaveLogic WaveManager => waveManager;
    public StartWaveState StartWaveState { get; private set; }
    public FirstWaveState FirstWaveState { get; private set; }
    public SecondWaveState SecondWaveState { get; private set; }
    public FinalWaveState FinalWaveState { get; private set; }
    public WinWaveState WinWaveState { get; private set; }
    public LoseWaveState LoseWaveState { get; private set; }

    Coroutine _waveSequenceCoroutine;

    private void Awake()
    {
        if (waveManager == null)
        {
            waveManager = FindAnyObjectByType<WaveLogic>();
        }
        StartWaveState = new StartWaveState(this);
        FirstWaveState = new FirstWaveState(this);
        SecondWaveState = new SecondWaveState(this);
        FinalWaveState = new FinalWaveState(this);
        WinWaveState = new WinWaveState(this);
        LoseWaveState = new LoseWaveState(this);
    }
    void OnEnable()
    {
        Debug.Log(waveManager != null);
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (CurrentState == null) return;
        CurrentState.Update();
        if (player.GetComponent<PlayerMovementMinigame>() == null) return;
        Debug.Log(player.GetComponent<PlayerMovementMinigame>().name);
        
        if (!player.GetComponent<PlayerMovementMinigame>().isAlive)
        {
            // stop the running sequence properly using the stored handle
            if (_waveSequenceCoroutine != null)
            {
                StopCoroutine(_waveSequenceCoroutine);
                _waveSequenceCoroutine = null;
            }

            ChangeState(LoseWaveState);
        }
        Debug.Log(CurrentState.GetType().Name);
    }
    public void ChangeState(WaveState newState)
    {
        if (newState == null) return;
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    IEnumerator RunWavesSequence()
    {
        // Wait 3 seconds then switch to first wave
        yield return new WaitForSeconds(3f);
        ChangeState(FirstWaveState);

        // Wait next 30 seconds (3 -> 33) then second wave
        yield return new WaitForSeconds(30f);
        ChangeState(SecondWaveState);

        // Wait next 30 seconds (33 -> 63) then final wave
        yield return new WaitForSeconds(30f);
        ChangeState(FinalWaveState);

        // Wait next 30 seconds (63 -> 93) then end
        yield return new WaitForSeconds(30f);
        ChangeState(WinWaveState);
    }
    public void StartMinigame()
    {
        minigame.SetActive(true);
        // use the pre-created StartWaveState instance instead of new'ing another one
        CurrentState = StartWaveState;
        CurrentState.Enter();

        // Start the wave sequence and keep the Coroutine handle so we can stop it later
        _waveSequenceCoroutine = StartCoroutine(RunWavesSequence());
    }
}
