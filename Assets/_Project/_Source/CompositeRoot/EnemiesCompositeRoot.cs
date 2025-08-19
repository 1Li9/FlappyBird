using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCompositeRoot : CompositeRoot
{
    [SerializeField] private EnemySpawnTrigger _spawnTrigger;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _spawnHeight;

    [SerializeField] private EntityReleaserView<Enemy> _enemyReleaserView;
    [SerializeField] private EntityReleaserView<EnemyBullet> _bulletReleaserView;

    [SerializeField] private WorldMoverCompositeRoot _worldMoverCompositeRoot;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;
    [SerializeField] private CollisionProcessorCompositeRoot _collisionProcessorCompositeRoot;

    [SerializeField] private WeaponConfig _weaponConfig;

    [SerializeField] private EntityViewFabric _viewFabric;

    private Spawner<Enemy> _spawner;
    private List<Enemy> _spawnedEnemies;
    private EntityReleaser<Enemy> _enemyReleaser;

    private BulletFabric _bulletFabric;
    private MoveSimulation<EnemyBullet> _bulletSimulation;
    private IObjectPool<EnemyBullet> _bulletPool;

    private void OnDisable()
    {
        _servicesRoot.Tick.Remove(_bulletSimulation);
        _servicesRoot.Dispose.Remove(_bulletSimulation);

        foreach (Enemy enemy in _spawnedEnemies)
            enemy.Dead -= OnDead;
    }

    public override void Composite()
    {
        _spawnedEnemies = new List<Enemy>();

        _bulletFabric = new BulletFabric();
        _bulletPool = new ObjectPool<EnemyBullet>(BulletFabric);
        _bulletSimulation = new MoveSimulation<EnemyBullet>(_weaponConfig.BulletDirection, _weaponConfig.BulletSpeed);

        EntityReleaser<EnemyBullet> bulletReleaser = new(_bulletPool);
        _bulletReleaserView.Bind(bulletReleaser);

        ObjectPool<Enemy> pool = new(EnemyFabric);
        _enemyReleaser = new EntityReleaser<Enemy>(pool);
        _enemyReleaserView.Bind(_enemyReleaser);

        _spawner = new Spawner<Enemy>(_spawnTrigger, pool);
        _servicesRoot.Pause.Add(_spawner);
        _servicesRoot.Dispose.Add(_spawner);

        MoveSimulation<IPositional> worldMover = _worldMoverCompositeRoot.Simulation;
        worldMover.Add(_spawnTrigger);

        EnemySpawnStrategy spawnStrategy = new(worldMover, _spawnTransform.position, _spawnHeight);
        _spawner.SetStrategy(spawnStrategy);

        _servicesRoot.Tick.Add(_bulletSimulation);
        _servicesRoot.Dispose.Add(_bulletSimulation);
    }

    private Enemy EnemyFabric(Vector3 position)
    {
        if (_bulletPool == null)
            throw new InvalidOperationException(nameof(EnemyFabric));

        Enemy enemy = new();
        enemy.SetPosition(position);
        enemy.SetScale(Vector3.one);

        Weapon<EnemyBullet> weapon = new(_bulletPool, _bulletSimulation, _weaponConfig.XBulletSpawnGap);
        weapon.Bind(enemy);

        _servicesRoot.Timers.Create(weapon.Shoot, _weaponConfig.CooldownTime);

        _viewFabric.Create(enemy);

        enemy.Dead += OnDead;
        _spawnedEnemies.Add(enemy);

        return enemy;
    }

    private EnemyBullet BulletFabric(Vector3 position)
    {
        EnemyBullet bullet = _bulletFabric.Create(() => new EnemyBullet(), position);

        _viewFabric.Create(bullet);
        _servicesRoot.Dispose.Add(_viewFabric.Animator);
        _servicesRoot.Pause.Add(_viewFabric.Animator);

        return bullet;
    }

    private void OnDead(Enemy enemy)
    {
        _enemyReleaser.Release(enemy);
    }
}