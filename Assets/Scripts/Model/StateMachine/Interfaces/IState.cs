public interface IState : ITickable
{
    public void Enter();

    public void Exit();

    public void BindTransitions(params ITransition[] transitions);
}