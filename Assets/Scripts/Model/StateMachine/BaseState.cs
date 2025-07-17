public abstract class BaseState : IState
{
    private readonly IStateChanger _stateChanger;
    private ITransition[] _transitions;

    protected BaseState(IStateChanger stateChanger) =>
        _stateChanger = stateChanger;

    public void BindTransitions(params ITransition[] transitions) =>
        _transitions = transitions;

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick(float deltaTime)
    {
        foreach (ITransition transition in _transitions)
            if (transition.TryGetNextState(out IState nextState))
                _stateChanger.ChangeState(nextState);
    }
}