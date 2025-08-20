using UnityEngine;

public class Weapon
{
    private readonly IObjectPool<Bullet> _pool;
    private readonly ISimulation<Bullet> _simulation;
    private readonly float _xBulletSpawnGap;

    private IPositional _positional;
    private float _currentTime;

    public Weapon(IObjectPool<Bullet> pool, ISimulation<Bullet> simulation, float xBulletSpawnGap)
    {
        _pool = pool;
        _simulation = simulation;
        _xBulletSpawnGap = xBulletSpawnGap;
    }

    public void Shoot()
    {
        if (_positional == null)
            throw new System.InvalidOperationException(nameof(Shoot));

        Vector3 position = new(_positional.Position.x + _xBulletSpawnGap, _positional.Position.y, _positional.Position.z);
        Bullet bullet = _pool.Get(position);

        if (_simulation.Contains(bullet) == false)
            _simulation.Add(bullet);
    }

    public void Bind(IPositional entity)
    {
        _positional = entity;
    }
}