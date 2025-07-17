public interface ITransition
{
    public bool TryGetNextState(out IState nextState);
}