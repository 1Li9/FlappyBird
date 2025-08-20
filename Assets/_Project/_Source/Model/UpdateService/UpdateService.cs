using System;
using System.Collections.Generic;

public class UpdateService : IDisposable, IPausable
{
    private readonly List<ITickable> _tickables = new();
    private readonly List<IUpdatable> _updatables = new();

    private bool _isActive = true;

    public void Tick(float deltaTime)
    {
        if (_isActive == false)
            return;

        for (int i = _tickables.Count - 1; i >= 0; i--)
        {
            ITickable tickable = _tickables[i];
            tickable.Tick(deltaTime);
        }
    }

    public void Update()
    {
        if (_isActive == false)
            return;

        for (int i = _updatables.Count - 1; i >= 0; i--)
        {
            IUpdatable updatable = _updatables[i];
            updatable.Update();
        }
    }

    public void Add(ITickable tickable)
    {
        _tickables.Add(tickable);
    }

    public void Add(IUpdatable updatable)
    {
        _updatables.Add(updatable);
    }

    public void Remove(ITickable tickable)
    {
        _tickables.Remove(tickable);
    }

    public void Remove(IUpdatable updatable)
    {
        _updatables.Remove(updatable);
    }

    public void Dispose()
    {
        _tickables.Clear();
        _updatables.Clear();
    }

    public void Pause()
    {
        _isActive = false;
    }

    public void Play()
    {
        _isActive = true;
    }
}