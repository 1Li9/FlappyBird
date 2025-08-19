using System.Collections.Generic;
using UnityEngine;

public class MoveSimulation<T> : ISimulation<T> where T : class, IPositional
{
    private readonly Vector3 _direction;
    private readonly float _speed;

    private readonly List<T> _objs;

    public MoveSimulation(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        _objs = new List<T>();
    }

    public void Dispose()
    {
        _objs.Clear();
    }
        
    public void Tick(float deltaTime)
    {
        foreach (T obj in _objs)
        {
            Vector3 position = obj.Position;
            Vector3 newPosition = position + _direction;
            obj.SetPosition(Vector3.MoveTowards(position, newPosition, _speed * deltaTime));
        }
    }

    public void Add(T obj)
    {
        _objs.Add(obj);
    }

    public void Remove(T obj)
    {
        _objs.Remove(obj);
    }

    public bool Contains(T obj)
    {
        return _objs.Contains(obj);
    }
}