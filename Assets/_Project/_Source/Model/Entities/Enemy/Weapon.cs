using UnityEngine;

public class Weapon : ITickable, IPausable
{
    private readonly IObjectPool<Bullet> _pool;
    private readonly ISimulation<IPositional> _simulation;
    private readonly float _cooldownTime;
    private readonly float _xBulletSpawnGap;

    private IPositional _positional;
    private float _currentTime;
    private bool _isActive = true;

    public Weapon(IObjectPool<Bullet> pool, ISimulation<IPositional> simulation, float cooldownTime, float xBulletSpawnGap)
    {
        _pool = pool;
        _simulation = simulation;
        _cooldownTime = cooldownTime;
        _xBulletSpawnGap = xBulletSpawnGap;
    }

    public void Pause()
    {
        _isActive = false;
    }

    public void Play()
    {
        _isActive = true;
    }

    public void Tick(float deltaTime)
    {
        if (_positional == null)
            throw new System.InvalidOperationException(nameof(Tick));

        _currentTime -= deltaTime;

        if (_currentTime > 0 | _isActive == false)
            return;

        _currentTime = _cooldownTime;

        Bullet bullet = _pool.Get();
        Vector3 position = new(_positional.Position.x + _xBulletSpawnGap, _positional.Position.y, _positional.Position.z);
        bullet.SetPosition(position);

        if (_simulation.Contains(bullet) == false)
            _simulation.Add(bullet);
    }

    public void Bind(IPositional entity)
    {
        _positional = entity;
    }
}