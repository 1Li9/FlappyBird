using UnityEngine;

public class Weapon<T> where T : Bullet
{
    private readonly IObjectPool<T> _pool;
    private readonly ISimulation<T> _simulation;
    private readonly float _xBulletSpawnGap;

    private IPositional _positional;
    private float _currentTime;

    public Weapon(IObjectPool<T> pool, ISimulation<T> simulation, float xBulletSpawnGap)
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
        T bullet = _pool.Get(position);

        if (_simulation.Contains(bullet) == false)
            _simulation.Add(bullet);
    }

    public void Bind(IPositional entity)
    {
        _positional = entity;
    }
}