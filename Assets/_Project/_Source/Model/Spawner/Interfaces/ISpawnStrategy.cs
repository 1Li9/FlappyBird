using System;

public interface ISpawnStrategy<T> where T : class, ITransformable
{
    public void Spawn(T obj);
}