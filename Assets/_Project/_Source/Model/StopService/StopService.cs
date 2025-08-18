using System.Collections.Generic;

public class StopService
{
    private readonly List<IStopable> _stopables;

    public StopService()
    {
        _stopables = new List<IStopable>();
    }

    public void Add(IStopable stopable)
    {
        _stopables.Add(stopable);
    }

    public void Remove(IStopable stopable)
    {
        _stopables.Remove(stopable);
    }

    public void Stop()
    {
        foreach (IStopable stopable in _stopables)
        {
            if(stopable == null)
                throw new System.NullReferenceException(nameof(stopable));

            stopable.Stop();
        }
    }
}