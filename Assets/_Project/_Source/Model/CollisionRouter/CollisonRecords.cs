using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisonRecords
{
    private readonly StopService _stopService;

    public CollisonRecords(StopService stopService)
    {
        _stopService = stopService;
    }

    public event Action GameStopped;

    public IEnumerable<IRecord> Get()
    {
        yield return GetRecord((Bird bird, Bullet bullet) =>
        {
            _stopService.Stop();

            GameStopped?.Invoke();
        });
    }

    private Record<T1, T2> GetRecord<T1, T2>(Action<T1, T2> action)
    {
        return new Record<T1, T2>(action);
    }
}