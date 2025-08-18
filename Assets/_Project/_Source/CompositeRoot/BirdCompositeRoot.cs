using UnityEngine;

public class BirdCompositeRoot : CompositeRoot
{
    private const string Jump = nameof(Jump);

    [SerializeField] private BirdConfig _config;
    [SerializeField] private EntityViewFabric _viewFabric;

    [SerializeField] private GravityCompositeRoot _gravityCompositeRoot;
    [SerializeField] private CollisionProcessorCompositeRoot _collisionProcessorCompositeRoot;

    private Bird _bird;
    private BirdJumper _jumper;
    private BirdRotator _rotator;
    private BirdInputRouter _inputRouter;
    private EntityAnimator _animator;

    private void OnDisable()
    {
        _inputRouter.Disable();
        _jumper.OnJump -= OnJump;
    }

    private void OnEnable()
    {
        if(_inputRouter == null || _jumper == null) 
            return;

        _inputRouter.Enable();
        _jumper.OnJump += OnJump;
    }

    private void Update()
    {
        _rotator.Tick(Time.deltaTime);
    }

    public override void Composite()
    {
        _bird = new Bird();
        _bird.SetScale(Vector3.one);
        _bird.SetPosition(_config.StartPosition);

        _jumper = new BirdJumper(_bird, _config.JumpForce);
        _rotator = new BirdRotator(_config.MaxRotationAngle, _config.MinRotationAngle, _config.RotationSpeed, _bird);
        _inputRouter = new BirdInputRouter(_jumper, _rotator);

        _gravityCompositeRoot.Simulation.Add(_bird);

        EntityView view = _viewFabric.Create(_bird);
        _animator = _viewFabric.Animator;

        _jumper.OnJump += OnJump;
    }

    private void OnJump()
    {
        _animator.SetTigger(Jump);
    }
}