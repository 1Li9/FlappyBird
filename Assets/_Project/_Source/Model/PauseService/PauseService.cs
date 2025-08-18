using System;
using System.Collections.Generic;

public class PauseService : IDisposable
{
    private readonly List<IPausable> _pausables;

    public bool IsPaused { get; private set; }

    public PauseService()
    {
        _pausables = new List<IPausable>();
    }

    public void Dispose()
    {
        _pausables.Clear();
    }

    public void Add(IPausable pausable)
    {
        _pausables.Add(pausable);
    }

    public void Remove(IPausable pausable)
    {
        _pausables.Remove(pausable);
    }

    public void Pause()
    {
        IsPaused = true;

        foreach (IPausable pausable in _pausables)
            pausable.Pause();
    }

    public void Play()
    {
        IsPaused = false;

        foreach (IPausable pausable in _pausables)
            pausable.Play();
    }
}