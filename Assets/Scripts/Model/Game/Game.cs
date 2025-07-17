public class Game : ITickable
{
    private StateMachine _stateMachine;

    public Game(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Start()
    {
        _stateMachine.Enter();
    }

    public void Tick(float deltaTime)
    {
        _stateMachine.Tick(deltaTime);
    }
}