public class StartWaveState : WaveState
{
    public StartWaveState(MinigameStateManager stateManager) : base(stateManager){ }
    public override void Enter()
    {
        // Start the countdown on the state manager so it isn't affected by
        // StopAllCoroutines() calls on the WaveLogic component.
        stateManager.StartCoroutine(stateManager.WaveManager.StartGameCountDown(3));
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
    }
}
