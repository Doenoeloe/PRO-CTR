public class LoseWaveState : WaveState
{
    public LoseWaveState(MinigameStateManager stateManager) : base(stateManager) { }
    public override void Enter()
    {
        stateManager.WaveManager.StartCoroutine(stateManager.WaveManager.DisplayEndGameMessage(2f));
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
    }
}
