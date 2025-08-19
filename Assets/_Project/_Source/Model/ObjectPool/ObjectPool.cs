using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectPool<T> where T : class, IEntity
{
    private readonly Func<Vector3, T> _factory;
    private readonly Queue<T> _releasedObjs;

    public ObjectPool(Func<Vector3, T> factory)
    {
        _factory = factory;
        _releasedObjs = new Queue<T>(); 
    }

    public T Get(Vector3 position)
    {
        if (_releasedObjs.Count == 0)
            return _factory?.Invoke(position);

        T obj = _releasedObjs.Dequeue();
        obj.SetPosition(position);
        obj.Enable();

        return obj;
    }

    public void Release(T obj)
    {
        _releasedObjs.Enqueue(obj);
        obj.Disable();
    }

    public void Dispose()
    {
        _releasedObjs.Clear();
    }
}