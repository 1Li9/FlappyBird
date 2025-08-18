using System;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulation : ISimulation<IPhysical>
{
    private readonly List<IPhysical> _objs;
    private readonly IGravityConfig _config;

    private bool _isPaused;

    public GravitySimulation(IGravityConfig config)
    {
        _objs = new List<IPhysical>();
        _config = config;
    }

    public void Tick(float deltaTime)
    {
        if (_isPaused)
            return;

        foreach (IPhysical obj in _objs)
        {
            SimulateVelocity(obj);
            obj.SetPosition(obj.Position + obj.Velocity * deltaTime);
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

    public void Add(IPhysical obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        _objs.Add(obj);
    }

    public void Remove(IPhysical obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        _objs.Remove(obj);
    }

    public bool Contains(IPhysical obj)
    {
        return _objs.Contains(obj);
    }

    private void SimulateVelocity(IPhysical obj)
    {
        Vector3 velocity = obj.Velocity - new Vector3(0, _config.GravityAccelerationScale, 0);

        if (velocity.y > _config.MaxFlySpeed)
            velocity.y = _config.MaxFlySpeed;
        if(velocity.y < -_config.MaxFalllSpeed)
            velocity.y = - _config.MaxFalllSpeed;

        obj.SetVelocity(velocity);
    }
}