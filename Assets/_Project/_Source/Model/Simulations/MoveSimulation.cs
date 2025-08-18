using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveSimulation : IDisposable, ISimulation<IPositional>
{
    private readonly Vector3 _direction;
    private readonly float _speed;

    private readonly List<IPositional> _objs;

    private bool _isPaused;

    public MoveSimulation(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        _objs = new List<IPositional>();
    }

    public void Dispose()
    {
        Stop();
    }
        
    public void Tick(float deltaTime)
    {
        if (_isPaused)
            return;

        foreach (IPositional obj in _objs)
        {
            Vector3 position = obj.Position;
            Vector3 newPosition = position + _direction;
            obj.SetPosition(Vector3.MoveTowards(position, newPosition, _speed * deltaTime));
        }
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Play()
    {
        _isPaused = false;
    }

    public void Stop()
    {
        _objs.Clear();
    }

    public void Add(IPositional obj)
    {
        _objs.Add(obj);
    }

    public void Remove(IPositional obj)
    {
        _objs.Remove(obj);
    }

    public bool Contains(IPositional obj)
    {
        return _objs.Contains(obj);
    }
}