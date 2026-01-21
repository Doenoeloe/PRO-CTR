public class FirstWaveState : WaveState
{
    public FirstWaveState(MinigameStateManager stateManager) : base(stateManager) { }
    public override void Enter()
    {
        stateManager.WaveManager.InvokeRepeating(nameof(stateManager.WaveManager.RandomBulletSpawner), 2.0f, 1.0f);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        stateManager.WaveManager.CancelInvoke(nameof(stateManager.WaveManager.RandomBulletSpawner));
    }
}
