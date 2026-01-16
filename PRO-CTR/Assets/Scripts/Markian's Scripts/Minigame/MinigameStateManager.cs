using UnityEngine;
public class MinigameStateManager : MonoBehaviour
{
    public enum PhaseStates
    {
        Start,
        FirstWave,
        SecondWave,
        FinalWave,
        End
    }
    void EnterState(PhaseStates state)
    {
        switch (state)
        {
            case PhaseStates.Start:
                //start a countdown;
                //spawn a player;
                break;
            case PhaseStates.FirstWave:
                //SpawnRandom bullets method;
                //Spawn 3 times a bullet walls;
                    break;
            case PhaseStates.SecondWave:
                //spawn Random bullets for about 10 seconds
                //Spawn a circle of bullets and each bullet with period of 0.5 seconds launches to the player;
                //Spawn Lazerblades that will shoot after 2 seconds;
                break;
            case PhaseStates.FinalWave:
                //spawn Random bullets for about 10 seconds
                //Spawn a circle of bullets and each bullet with period of 0.5 seconds launches to the player;
                //Walls become closer to player;
                //Lazerblades;

                break;
            case PhaseStates.End:
                //Message like:"Nice" 
                //Switch scene;
                break;
        }
    }
    void ExitState(PhaseStates state) { }
}
