using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : ITickable
{
    private readonly Action _action;
    private readonly float _timeDelay;

    private float _currentTime;

    public Timer(Action action, float timeDelay)
    {
        _action = action;
        _timeDelay = timeDelay;
    }

    public void Tick(float deltaTime)
    {
        _currentTime -= deltaTime;

        if (_currentTime > 0)
            return;

        _currentTime = _timeDelay;
        _action();
    }
}
