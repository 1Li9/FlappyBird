using System;
using System.Collections.Generic;

public class TickService : IDisposable, IPausable
{
    private readonly List<ITickable> _tickables = new();

    private bool _isActive = true;

    public void Tick(float deltaTime)
    {
        if (_isActive == false)
            return;

        foreach (var tickable in _tickables)
            tickable.Tick(deltaTime);
    }

    public void Add(ITickable tickable)
    {
        _tickables.Add(tickable);
    }

    public void Remove(ITickable tickable)
    {
        _tickables.Remove(tickable);
    }

    public void Dispose()
    {
        _tickables.Clear();
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