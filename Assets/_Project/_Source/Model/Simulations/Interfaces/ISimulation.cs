using System;

public interface ISimulation : ITickable, IDisposable
{
}

public interface ISimulation<T> : ISimulation
{
    public void Add(T item);  
    public void Remove(T item);
    public bool Contains(T item);
}