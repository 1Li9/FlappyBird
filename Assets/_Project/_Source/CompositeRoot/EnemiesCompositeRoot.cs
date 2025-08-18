using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCompositeRoot : CompositeRoot
{
    [SerializeField] private EnemySpawnTrigger _spawnTrigger;
    [SerializeField] private EntityReleaser<Enemy> _enemyReleaser;
    [SerializeField] private EntityReleaser<Bullet> _bulletReleaser;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _spawnHeight;

    [SerializeField] private WorldMoverCompositeRoot _worldMoverCompositeRoot;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;
    [SerializeField] private CollisionProcessorCompositeRoot _collisionProcessorCompositeRoot;

    [SerializeField] private WeaponConfig _weaponConfig;
    [SerializeField] private WeaponsView _weaponsView;
    [SerializeField] private BulletSimulationCompositeRoot _bulletSimulation;

    [SerializeField] private EntityViewFabric _viewFabric;

    private Spawner<Enemy> _spawner;
    private List<IDisposable> _disposables;

    private IObjectPool<Bullet> _bulletPool;

    private void OnDisable()
    {
        foreach (IDisposable disposable in _disposables)
            disposable.Dispose();
    }

    public override void Composite()
    {
        _disposables = new List<IDisposable>();

        _bulletPool = new ObjectPool<Bullet>(BulletFabric);
        _bulletReleaser.BindPool(_bulletPool);

        ObjectPool<Enemy> pool = new(EnemyFabric);
        _enemyReleaser.BindPool(pool);

        _spawner = new Spawner<Enemy>(_spawnTrigger, pool);
        _servicesRoot.Pause.Add(_spawner);
        _servicesRoot.Stop.Add(_spawner);
        _disposables.Add(_spawner);

        MoveSimulation worldMover = _worldMoverCompositeRoot.Simulation;
        worldMover.Add(_spawnTrigger);

        EnemySpawnStrategy spawnStrategy = new(worldMover, _spawnTransform.position, _spawnHeight);
        _spawner.SetStrategy(spawnStrategy);
    }

    private Enemy EnemyFabric()
    {
        if (_bulletPool == null)
            throw new InvalidOperationException(nameof(EnemyFabric));

        Enemy enemy = new();
        enemy.SetScale(Vector3.one);

        Weapon weapon = new(_bulletPool, _bulletSimulation.Simulation, _weaponConfig.CooldownTime, _weaponConfig.XBulletSpawnGap);
        weapon.Bind(enemy);
        _weaponsView.Add(weapon);
        _servicesRoot.Pause.Add(weapon);
        _servicesRoot.Stop.Add(_weaponsView);

        _viewFabric.Create(enemy);

        return enemy;
    }

    private Bullet BulletFabric()
    {
        Bullet bullet = new();
        bullet.SetPosition(_weaponConfig.BulletSpawnPosition);
        bullet.SetScale(Vector3.one);

        _viewFabric.Create(bullet);
        _servicesRoot.Stop.Add(_viewFabric.Animator);

        return bullet;
    }
}