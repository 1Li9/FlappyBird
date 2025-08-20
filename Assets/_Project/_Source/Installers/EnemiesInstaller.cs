using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInstaller : Installer
{
    [SerializeField] private EnemySpawnTrigger _spawnTrigger;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _spawnHeight;

    [SerializeField] private EntityReleaserView<Enemy> _enemyReleaserView;
    [SerializeField] private EntityReleaserView<Bullet> _bulletReleaserView;

    [SerializeField] private WorldMoverInstaller _worldMoverInstaller;
    [SerializeField] private ServicesInstaller _servicesInstaller;
    [SerializeField] private CollisionProcessorInstaller _collisionProcessorInstaller;

    [SerializeField] private WeaponConfig _weaponConfig;

    [SerializeField] private EntityViewFabric _viewFabric;

    private Spawner<Enemy> _spawner;
    private List<Enemy> _spawnedEnemies = new();
    private EntityReleaser<Enemy> _enemyReleaser;

    private BulletFabric _bulletFabric;
    private MoveSimulation<Bullet> _bulletSimulation;
    private IObjectPool<Bullet> _bulletPool;

    private void OnDisable()
    {
        _servicesInstaller.UpdateSevice.Remove(_bulletSimulation);
        _servicesInstaller.Dispose.Remove(_bulletSimulation);

        foreach (Enemy enemy in _spawnedEnemies)
            enemy.Dead -= OnDead;
    }

    public override void Install()
    {
        _bulletFabric = new BulletFabric();
        _bulletPool = new ObjectPool<Bullet>(BulletFactory);
        _bulletSimulation = new MoveSimulation<Bullet>(_weaponConfig.BulletDirection, _weaponConfig.BulletSpeed);

        EntityReleaser<Bullet> bulletReleaser = new(_bulletPool);
        _bulletReleaserView.Bind(bulletReleaser);

        ObjectPool<Enemy> pool = new(EnemyFactory);
        _enemyReleaser = new EntityReleaser<Enemy>(pool);
        _enemyReleaserView.Bind(_enemyReleaser);

        _spawner = new Spawner<Enemy>(_spawnTrigger, pool);
        _servicesInstaller.Pause.Add(_spawner);
        _servicesInstaller.Dispose.Add(_spawner);

        MoveSimulation<IPositional> worldMover = _worldMoverInstaller.Simulation;
        worldMover.Add(_spawnTrigger);

        EnemySpawnStrategy spawnStrategy = new(worldMover, _spawnTransform.position, _spawnHeight);
        _spawner.SetStrategy(spawnStrategy);

        _servicesInstaller.UpdateSevice.Add(_bulletSimulation);
        _servicesInstaller.Dispose.Add(_bulletSimulation);
    }

    private Enemy EnemyFactory(Vector3 position)
    {
        if (_bulletPool == null)
            throw new InvalidOperationException(nameof(EnemyFactory));

        Enemy enemy = new();
        enemy.SetPosition(position);
        enemy.SetScale(Vector3.one);

        Weapon weapon = new(_bulletPool, _bulletSimulation, _weaponConfig.XBulletSpawnGap);
        weapon.Bind(enemy);

        _servicesInstaller.Timers.Create(weapon.Shoot, _weaponConfig.CooldownTime);

        _viewFabric.Create(enemy);

        enemy.Dead += OnDead;
        _spawnedEnemies.Add(enemy);

        return enemy;
    }

    private Bullet BulletFactory(Vector3 position)
    {
        Bullet bullet = _bulletFabric.Create(() => new Bullet(), position);

        _viewFabric.Create(bullet);
        _servicesInstaller.Dispose.Add(_viewFabric.Animator);
        _servicesInstaller.Pause.Add(_viewFabric.Animator);

        return bullet;
    }

    private void OnDead(Enemy enemy)
    {
        _enemyReleaser.Release(enemy);
    }
}