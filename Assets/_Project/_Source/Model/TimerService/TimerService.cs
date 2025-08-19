using System;
using System.Collections.Generic;

public class TimerService : IDisposable, ITickable
{
    private readonly List<Timer> _timers;

    public TimerService()
    {
        _timers = new List<Timer>();
    }

    public Timer Create(Action action, float timeDelay)
    {
        Timer timer = new(action, timeDelay);
        _timers.Add(timer);

        return timer;
    }

    public void Stop(Timer timer)
    {
        _timers.Remove(timer);
    }

    public void Dispose()
    {
        _timers.Clear();
    }

    public void Tick(float deltaTime)
    {
        foreach (Timer timer in _timers)
            timer.Tick(deltaTime);
    }
}