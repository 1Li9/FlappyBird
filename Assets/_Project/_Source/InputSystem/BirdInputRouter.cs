using UnityEngine.InputSystem;

public class BirdInputRouter : IEnableable, IDisableable, IPausable
{
    private readonly BirdJumper _jumper;
    private readonly BirdRotator _rotator;
    private readonly Weapon _weapon;

    private readonly GameControlls _input;

    public BirdInputRouter(BirdJumper jumper, BirdRotator rotator, Weapon weapon)
    {
        _jumper = jumper;
        _rotator = rotator;
        _weapon = weapon;

        _input = new GameControlls();
    }

    public void Disable()
    {
        _input.Disable();
        _input.Bird.Jump.performed -= OnJump;
        _input.Bird.Shoot.performed -= OnShoot;
    }

    public void Enable()
    {
        _input.Enable();
        _input.Bird.Jump.performed += OnJump;
        _input.Bird.Shoot.performed += OnShoot;
    }

    public void Pause()
    {
        Disable();
    }

    public void Play()
    {
        Enable();
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _jumper.Jump();
        _rotator.OnJump();
    }

    private void OnShoot(InputAction.CallbackContext obj)
    {
        _weapon.Shoot();
    }
}
