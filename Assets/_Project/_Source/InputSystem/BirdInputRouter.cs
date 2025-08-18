using UnityEngine.InputSystem;

public class BirdInputRouter : IEnableable, IDisableable
{
    private readonly BirdJumper _jumper;
    private readonly BirdRotator _rotator;
    private readonly GameControlls _input;

    public BirdInputRouter(BirdJumper jumper, BirdRotator rotator)
    {
        _jumper = jumper;
        _rotator = rotator;
        _input = new GameControlls();
    }

    public void Disable()
    {
        _input.Disable();
        _input.Bird.Jump.performed -= OnJump;
    }

    public void Enable()
    {
        _input.Enable();
        _input.Bird.Jump.performed += OnJump;
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _jumper.Jump();
        _rotator.OnJump();
    }
}
