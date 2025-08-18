public interface ISimulation : ITickable, IPausable, IStopable
{
}

public interface ISimulation<T> : ISimulation
{
    public void Add(T item);  
    public void Remove(T item);
    public bool Contains(T item);
}