using System;
using UnityEngine;

public interface IObjectPool<T> : IDisposable  where T : class
{
    public T Get(Vector3 position);
    public void Release(T obj);
}
