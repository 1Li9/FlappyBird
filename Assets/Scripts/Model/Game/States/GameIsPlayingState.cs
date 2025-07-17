using UnityEngine;

public class GameIsPlayingState : BaseState
{
    private ISimulation[] _simulations;

    public GameIsPlayingState(IStateChanger changer, params ISimulation[] simulations) : base(changer)
    {
        _simulations = simulations;
    }

    public override void Enter()
    {
        Debug.Log(nameof(GameIsPlayingState) + " " + nameof(Enter));
    }

    public override void Exit()
    {
        Debug.Log(nameof(GameIsPlayingState) + " " + nameof(Exit));
    }

    public override void Tick(float deltaTIme)
    {
        base.Tick(deltaTIme);

        foreach (ISimulation simulation in _simulations)
            simulation.Simulate(deltaTIme);
    }

}