using System;

public class Spawner<T> : IDisposable, IPausable, IStopable where T : class, ITransformable
{
    private readonly ISpawnEventPublisher _spawnEvent;
    private readonly IObjectPool<T> _pool;

    private ISpawnStrategy<T> _strategy;
    private bool _isPaused;

    public Spawner(ISpawnEventPublisher spawnEvent, IObjectPool<T> pool)
    {
        _spawnEvent = spawnEvent;
        _pool = pool;
        _spawnEvent.Spawning += Spawn;
    }

    public void Dispose()
    {
        _spawnEvent.Spawning -= Spawn;
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Play()
    {
        _isPaused = false;
    }

    public void SetStrategy(ISpawnStrategy<T> strategy)
    {
        _strategy = strategy;
    }

    public void Stop()
    {
        _pool.Clear();
    }

    private void Spawn()
    {
        if (_isPaused)
            return;

        if(_strategy == null)
            throw new NullReferenceException(nameof(_strategy));

        T obj = _pool.Get();
        _strategy.Spawn(obj);
    }
}