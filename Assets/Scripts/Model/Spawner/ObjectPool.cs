using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : class, IEnableable, IDisposable, ITransformable
{
    private readonly Queue<T> _deactiveObjs;
    private readonly List<T> _activeObjs;

    public IReadOnlyList<T> ActiveObjs => _activeObjs;

    public ObjectPool(params T[] objs)
    {
        _deactiveObjs = new Queue<T>();
        _activeObjs = new List<T>();

        foreach (T obj in objs)
        {
            _deactiveObjs.Enqueue(obj);
            obj.Dispose();
        }
    }

    public bool TryGet(out T obj)
    {
        if (_deactiveObjs.TryDequeue(out obj) == false)
            return false;

        obj.Enable();
        ResetProperties(obj);
        _activeObjs.Add(obj);

        return true;
    }

    public void Release(T obj)
    {
        if (_activeObjs.Contains(obj) == false)
            throw new System.ArgumentException(nameof(obj));

        ResetProperties(obj);
        _activeObjs.Remove(obj);
        _deactiveObjs.Enqueue(obj);

        obj.Dispose();
    }

    private void ResetProperties(T obj)
    {
        obj.SetPosition(Vector3.zero);
        obj.SetRotation(Vector3.zero);
    }
}