public class GameFabric
{
    private GameStateMachineFabric _stateMachineFabric;

    public GameFabric(params ISimulation[] simulations)
    {
        _stateMachineFabric = new(simulations);
    }

    public Game Create()
    {
        return new(_stateMachineFabric.Create());
    }
}