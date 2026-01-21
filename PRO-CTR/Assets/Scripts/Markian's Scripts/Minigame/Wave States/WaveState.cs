public abstract class WaveState
{
    protected MinigameStateManager stateManager;
    public WaveState(MinigameStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Enter() { }
    // Update is called once per frame
    public virtual void Update() { }
    public virtual void Exit() { }
}
