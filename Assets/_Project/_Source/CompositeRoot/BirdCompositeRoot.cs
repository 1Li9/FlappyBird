using System.Collections.Generic;
using UnityEngine;

public class BirdCompositeRoot : CompositeRoot
{
    private const string Jump = nameof(Jump);

    [SerializeField] private BirdConfig _config;
    [SerializeField] private EntityViewFabric _viewFabric;

    [SerializeField] private GravityCompositeRoot _gravityCompositeRoot;
    [SerializeField] private CollisionProcessorCompositeRoot _collisionProcessorCompositeRoot;
    [SerializeField] private EntityReleaserView<BirdBullet> _bulletReleaserView;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;

    private Bird _bird;
    private BirdJumper _jumper;
    private BirdRotator _rotator;
    private BirdInputRouter _inputRouter;
    private EntityAnimator _animator;

    private BulletFabric _bulletFabric = new();
    private ObjectPool<BirdBullet> _bulletPool;
    private List<BirdBullet> _spawnedBullets = new();
    private EntityReleaser<BirdBullet> _bulletReleaser;
    private Weapon<BirdBullet> _weapon;
    private BulletSimulation<BirdBullet> _bulletSimulation;

    private void OnDisable()
    {
        _servicesRoot.Tick.Remove(_bulletSimulation);
        _servicesRoot.Tick.Remove(_rotator);
        _servicesRoot.Pause.Remove(_inputRouter);

        _inputRouter.Disable();
        _jumper.OnJump -= OnJump;

        foreach (BirdBullet bullet in _spawnedBullets)
            bullet.Dead -= _bulletReleaser.Release;
    }

    private void OnEnable()
    {
        if (_inputRouter == null || _jumper == null)
            return;

        _inputRouter.Enable();
        _jumper.OnJump += OnJump;
    }

    public override void Composite()
    {
        _bird = new Bird();
        _bird.SetScale(Vector3.one);
        _bird.SetPosition(_config.StartPosition);

        _bulletSimulation = new BulletSimulation<BirdBullet>(_config.BuletSpeed);
        _bulletPool = new ObjectPool<BirdBullet>(BulletFabric);
        _weapon = new Weapon<BirdBullet>(_bulletPool, _bulletSimulation, _config.BulletXSpawnGap);
        _weapon.Bind(_bird);

        _bulletReleaser = new(_bulletPool);
        _bulletReleaserView.Bind(_bulletReleaser);

        _jumper = new BirdJumper(_bird, _config.JumpForce);
        _rotator = new BirdRotator(_config.MaxRotationAngle, _config.MinRotationAngle, _config.RotationSpeed, _bird);
        _inputRouter = new BirdInputRouter(_jumper, _rotator, _weapon);

        _gravityCompositeRoot.Simulation.Add(_bird);

        EntityView view = _viewFabric.Create(_bird);
        _animator = _viewFabric.Animator;

        _jumper.OnJump += OnJump;

        _servicesRoot.Tick.Add(_bulletSimulation);
        _servicesRoot.Tick.Add(_rotator);
        _servicesRoot.Pause.Add(_inputRouter);
    }

    private void OnJump()
    {
        _animator.SetTigger(Jump);
    }

    private BirdBullet BulletFabric(Vector3 position)
    {
        BirdBullet bullet = _bulletFabric.Create(() => new BirdBullet(), position);
        bullet.SetScale(new Vector3(-1,1,1));

        Vector3 angle = _bird.Rotation * Vector3.right;
        bullet.SetDirection(angle);
        bullet.SetRotation(_bird.Rotation);

        bullet.Dead += _bulletReleaser.Release;
        _spawnedBullets.Add(bullet);

        _viewFabric.Create(bullet);

        return bullet;
    }

}