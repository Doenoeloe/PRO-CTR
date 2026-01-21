using UnityEngine;

public class SecondWaveState : WaveState
{
    public SecondWaveState(MinigameStateManager stateManager) : base(stateManager) { }
    public override void Enter()
    {
        stateManager.WaveManager.InvokeRepeating(nameof(stateManager.WaveManager.RandomBulletSpawner), 2.0f, 0.5f);
        stateManager.WaveManager.InvokeRepeating(nameof(stateManager.WaveManager.SpawnProjectileWall), 5.0f, 1.0f);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        stateManager.WaveManager.CancelInvoke(nameof(stateManager.WaveManager.RandomBulletSpawner));
        stateManager.WaveManager.CancelInvoke(nameof(stateManager.WaveManager.SpawnProjectileWall));
    }
}
