using System;
using System.Collections.Generic;

public class ObjectPool<T> : IObjectPool<T> where T : class, IEntity
{
    private readonly Func<T> _factory;
    private readonly Queue<T> _objs;

    public ObjectPool(Func<T> factory)
    {
        _factory = factory;
        _objs = new Queue<T>(); 
    }

    public T Get()
    {
        if (_objs.Count == 0)
            return _factory?.Invoke();

        T obj = _objs.Dequeue();
        obj.Enable();

        return obj;
    }

    public void Release(T obj)
    {
        _objs.Enqueue(obj);
        obj.Disable();
    }

    public void Clear()
    {
        _objs.Clear();
    }
}