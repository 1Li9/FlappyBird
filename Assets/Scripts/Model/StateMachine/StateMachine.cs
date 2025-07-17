using System;
using System.Collections.Generic;

public class StateMachine : ITickable, IStateChanger
{
    private readonly Dictionary<Type, IState> _states;

    private IState _currentState;

    public StateMachine()
    {
        _states = new Dictionary<Type, IState>();
    }

    public void Enter()
    {
        if (_currentState == null)
            throw new InvalidOperationException(nameof(_currentState));

        _currentState.Enter();
    }

    public void Tick(float deltaTime)
    {
        _currentState.Tick(deltaTime);
    }

    public StateMachine AddState<T>(T state) where T : IState
    {
        if (_states.ContainsValue(state))
            throw new InvalidOperationException();

        _states.Add(typeof(T), state);

        return this;
    }

    public StateMachine BindTransitions<TFromState, TToState>(params Func<bool>[] conditions) where TFromState : IState where TToState : IState
    {
        if (_states.ContainsKey(typeof(TFromState)) == false || _states.ContainsKey(typeof(TToState)) == false)
            throw new InvalidOperationException();

        List<Transition> transitions = new List<Transition>();

        foreach (Func<bool> condition in conditions)
        {
            Transition transition = new(condition, _states[typeof(TToState)]);
            transitions.Add(transition);
        }

        _states[typeof(TFromState)].BindTransitions(transitions.ToArray());

        return this;
    }

    public void ChangeState(IState state)
    {
        if (_states.ContainsValue(state) == false || _currentState == null)
            throw new InvalidOperationException();

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void SetStartState<T>()
    {
        if (_states.ContainsKey(typeof(T)) == false)
            throw new InvalidOperationException();

        _currentState = _states[typeof(T)];
    }
}