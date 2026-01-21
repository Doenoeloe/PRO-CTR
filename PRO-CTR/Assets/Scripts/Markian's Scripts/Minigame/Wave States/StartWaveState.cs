public class StartWaveState : WaveState
{
    public StartWaveState(MinigameStateManager stateManager) : base(stateManager){ }
    public override void Enter()
    {
        stateManager.WaveManager?.StartCoroutine(stateManager.WaveManager.StartGameCountDown(3));
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
    }
}
