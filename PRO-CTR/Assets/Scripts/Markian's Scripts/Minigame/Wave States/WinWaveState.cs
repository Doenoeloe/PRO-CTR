public class WinWaveState : WaveState
{
    public WinWaveState(MinigameStateManager stateManager) : base(stateManager) { }
    public override void Enter()
    {
        stateManager.WaveManager.StopAllCoroutines();
        stateManager.WaveManager.StartCoroutine(stateManager.WaveManager.DisplayEndGameMessage(2f));
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
    }
}
