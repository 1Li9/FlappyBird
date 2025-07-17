using UnityEngine;

public class GameIsPausedState : BaseState
{
    public GameIsPausedState(IStateChanger stateChanger) : base(stateChanger)
    {
    }

    public override void Enter()
    {
        Debug.Log(nameof(GameIsPlayingState) + " " + nameof(Enter));
    }

    public override void Exit()
    {
        Debug.Log(nameof(GameIsPlayingState) + " " + nameof(Exit));
    }
}