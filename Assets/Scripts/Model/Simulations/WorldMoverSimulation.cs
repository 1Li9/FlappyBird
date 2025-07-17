using System.Collections.Generic;
using UnityEngine;

public class WorldMoverSimulation : ISimulation
{
    private Vector3 _direction;
    private float _speed;

    private List<IPositionable> _transformables;

    public WorldMoverSimulation(Vector3 direction, float speed)
    {
        _transformables= new List<IPositionable>();
        _direction = direction;
        _speed = speed;
    }

    public void Add(IPositionable transformable) =>
        _transformables.Add(transformable);

    public void Remove(IPositionable transformable)
    {
        if (_transformables.Contains(transformable))
            _transformables.Remove(transformable);
    }

    public void Simulate(float deltaTime)
    {
        foreach (IPositionable transformable in _transformables)
        {
            Vector3 position = _speed * deltaTime * _direction + transformable.Position;
            transformable.SetPosition(position);
        }
    }
}