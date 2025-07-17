using UnityEngine;

public class GameStateMachineFabric
{
    private ISimulation[] _simulations;

    public GameStateMachineFabric(params ISimulation[] simulations)
    {
        _simulations= simulations;
    }

    public StateMachine Create()
    {
        StateMachine stateMachine = new();

        stateMachine.AddState(new GameIsPlayingState(stateMachine, _simulations))
            .AddState(new GameIsPausedState(stateMachine))
            .BindTransitions<GameIsPlayingState, GameIsPausedState>(() => Input.GetKeyDown(KeyCode.Escape))
            .BindTransitions<GameIsPausedState, GameIsPlayingState>(() => Input.GetKeyDown(KeyCode.Escape))
            .SetStartState<GameIsPlayingState>();

        return stateMachine;
    }
}