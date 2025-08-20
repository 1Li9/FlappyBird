using System.Collections.Generic;
using UnityEngine;

public class BirdInstaller : Installer
{
    [SerializeField] private BirdConfig _config;
    [SerializeField] private EntityViewFabric _viewFabric;

    [SerializeField] private GravityInstaller _gravityInstaller;
    [SerializeField] private CollisionProcessorInstaller _collisionProcessorInstaller;
    [SerializeField] private EntityReleaserView<Bullet> _bulletReleaserView;
    [SerializeField] private ServicesInstaller _servicesRoot;

    private Bird _bird;
    private BirdJumper _jumper;
    private BirdRotator _rotator;
    private BirdInputRouter _inputRouter;
    private EntityAnimator _animator;

    private BulletFabric _bulletFabric = new();
    private ObjectPool<Bullet> _bulletPool;
    private List<Bullet> _spawnedBullets = new();
    private EntityReleaser<Bullet> _bulletReleaser;
    private Weapon _weapon;
    private BulletSimulation<Bullet> _bulletSimulation;

    private void OnDisable()
    {
        _servicesRoot.UpdateSevice.Remove(_bulletSimulation);
        _servicesRoot.UpdateSevice.Remove(_rotator);
        _servicesRoot.Pause.Remove(_inputRouter);

        _inputRouter.Disable();
        _jumper.Jumped -= OnJump;

        foreach (Bullet bullet in _spawnedBullets)
            bullet.Dead -= _bulletReleaser.Release;
    }

    private void OnEnable()
    {
        if (_inputRouter == null || _jumper == null)
            return;

        _inputRouter.Enable();
        _jumper.Jumped += OnJump;
    }

    public override void Install()
    {
        _bird = new Bird();
        _bird.SetScale(Vector3.one);
        _bird.SetPosition(_config.StartPosition);

        _bulletSimulation = new BulletSimulation<Bullet>(_config.BuletSpeed);
        _bulletPool = new ObjectPool<Bullet>(BulletFactory);
        _weapon = new Weapon(_bulletPool, _bulletSimulation, _config.BulletXSpawnGap);
        _weapon.Bind(_bird);

        _bulletReleaser = new(_bulletPool);
        _bulletReleaserView.Bind(_bulletReleaser);

        _jumper = new BirdJumper(_bird, _config.JumpForce);
        _rotator = new BirdRotator(_config.MaxRotationAngle, _config.MinRotationAngle, _config.RotationSpeed, _bird);
        _inputRouter = new BirdInputRouter(_jumper, _rotator, _weapon);

        _gravityInstaller.Simulation.Add(_bird);

        EntityView view = _viewFabric.Create(_bird);
        _animator = _viewFabric.Animator;

        _jumper.Jumped += OnJump;

        _servicesRoot.UpdateSevice.Add(_bulletSimulation);
        _servicesRoot.UpdateSevice.Add(_rotator);
        _servicesRoot.Pause.Add(_inputRouter);
    }

    private void OnJump()
    {
        _animator.SetTigger(EntityAnimator.AnimatorData.Jump);
    }

    private Bullet BulletFactory(Vector3 position)
    {
        Bullet bullet = _bulletFabric.Create(() => new Bullet(), position);
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