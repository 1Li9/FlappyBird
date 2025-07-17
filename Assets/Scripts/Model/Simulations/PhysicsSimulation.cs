using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSimulation : ISimulation
{
    private readonly List<IPhysical> _objs;
    private readonly IPhysicsConfig _config;

    public PhysicsSimulation(IPhysicsConfig config)
    {
        _objs = new List<IPhysical>();
        _config = config;
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

    public void ResolveCollision(Entity aObj, Entity bObj)
    {
        Debug.Log($"{aObj} столкнулся с {bObj}");
    }

    public void Simulate(float deltaTime)
    {

        foreach (IPhysical obj in _objs)
        {
            SimulateGravity(deltaTime, obj);

            Vector3 velocity = obj.Velocity;

            if (obj.Velocity.y > _config.MaxVerticalFlySpeed)
                velocity.y = _config.MaxVerticalFlySpeed;
            else if(obj.Velocity.y < -_config.MaxFallSpeed)
                velocity.y = -_config.MaxFallSpeed;

            obj.SetVelocity(velocity);
            obj.SetPosition(obj.Position + obj.Velocity * deltaTime);
        }
    }

    private void SimulateGravity(float deltaTime, IPhysical obj)
    {
        Vector3 velocity = obj.Velocity - new Vector3(0, _config.GravityAccelerationScale, 0);
        obj.SetVelocity(velocity);
    }
}