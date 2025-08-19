using System;

public class Spawner<T> : IDisposable, IPausable where T : class, ITransformable
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
        _pool.Dispose();
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

    private void Spawn()
    {
        if (_isPaused)
            return;

        if(_strategy == null)
            throw new NullReferenceException(nameof(_strategy));

        _strategy.Spawn(_pool.Get);
    }
}