using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSimulation
{
    private readonly List<IPhysical> _objs;
    private float _gravityAccelerationScale = .1f;
    private float _maxVerticalSpeed = 9f;

    public PhysicsSimulation()
    {
        _objs = new List<IPhysical>();
    }

    public IReadOnlyList<IPhysical> Objects => _objs;

    public event Action<IPhysical> ObjectAdded;
    public event Action<IPhysical> ObjectRemoved;

    public void Add(IPhysical obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        if (_objs.Contains(obj))
            return;

        _objs.Add(obj);
        ObjectAdded?.Invoke(obj);
    }

    public void Remove(IPhysical obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        if (_objs.Contains(obj) == false)
            return;

        _objs.Remove(obj);
        ObjectRemoved?.Invoke(obj);
    }

    public void ResolveCollision(IPhysical aObj, IPhysical bObj)
    {
        Debug.Log($"{aObj} столкнулся с {bObj}");
    }

    public void Simulate(float deltaTime)
    {
        foreach (IPhysical obj in _objs)
        {
            SimulateGravity(deltaTime, obj);

            obj.SetPosition(obj.Position + obj.Velocity * deltaTime);
        }
    }

    private void SimulateGravity(float deltaTime, IPhysical obj)
    {
        Vector3 velocity = obj.Velocity - new Vector3(0, _gravityAccelerationScale, 0);

        if (velocity.y < -_maxVerticalSpeed)
            return;

        obj.SetVelocity(velocity);
    }
}