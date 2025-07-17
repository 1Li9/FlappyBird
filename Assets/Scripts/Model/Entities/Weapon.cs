using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ITickable, IPositionable
{
    private readonly ObjectPool<Bullet> _pool;
    private readonly ObjectPoolReleaser<Bullet> _releaser;
    private readonly float _cooldownTime;
    private readonly BulletSimulation _bulletSimulation;
    private readonly IWeaponConfig _config;

    private float _currentTime;

    public event Action<Bullet> OnFired;

    public Vector3 Position {get; private set;}

    public Weapon(IWeaponConfig config, BulletSimulation bulletSimulation)
    {
        _config = config;
        _bulletSimulation = bulletSimulation;

        List<Bullet> bullets = new List<Bullet>();

        for (int i = 0; i < _config.PoolCapacity; i++)
        {
            Bullet bullet = new(Vector3.zero, Vector3.zero, Vector3.zero);
            bullets.Add(bullet);
        }

        _pool = new ObjectPool<Bullet>(bullets.ToArray());
        _releaser = new ObjectPoolReleaser<Bullet>(_pool, (xPosition) => xPosition < _config.ReleaseXPosition, _bulletSimulation.Remove);
    }

    public void Tick(float deltaTime)
    {
        _releaser.Update();

        if (_currentTime > 0)
        {
            _currentTime -= deltaTime;

            return;
        }

        if (_pool.TryGet(out Bullet bullet) == false)
            return;

        bullet.SetPosition(Position);
        _bulletSimulation.Add(bullet);
        OnFired?.Invoke(bullet);

        _currentTime = _cooldownTime;
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
    }
}