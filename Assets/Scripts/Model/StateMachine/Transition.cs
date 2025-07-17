using System;

public class Transition : ITransition
{
    private readonly Func<bool> _condition;
    private readonly IState _nextState;

    public Transition(Func<bool> condition, IState nextState)
    {
        _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        _nextState = nextState;
    }

    public bool TryGetNextState(out IState nextState)
    {
        nextState = null;

        if (_condition.Invoke() == false)
            return false;

        nextState = _nextState;

        return true;
    }
}