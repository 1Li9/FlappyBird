using System;
using System.Collections.Generic;

public class DisposeService
{
    private readonly List<IDisposable> _stopables;

    public DisposeService()
    {
        _stopables = new List<IDisposable>();
    }

    public void Add(IDisposable stopable)
    {
        _stopables.Add(stopable);
    }

    public void Remove(IDisposable stopable)
    {
        _stopables.Remove(stopable);
    }

    public void Dispose()
    {
        foreach (IDisposable stopable in _stopables)
        {
            if(stopable == null)
                throw new System.NullReferenceException(nameof(stopable));

            stopable.Dispose();
        }
    }
}