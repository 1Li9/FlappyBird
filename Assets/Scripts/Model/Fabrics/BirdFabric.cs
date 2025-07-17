public class BirdFabric
{
    private readonly IBirdConfig _config;
    private readonly IInputService _inputService;

    public BirdFabric(IBirdConfig config, IInputService input)
    {
        _config = config;

        if(config == null)
            throw new System.ArgumentNullException(nameof(config));

        _inputService = input;
    }

    public Bird Create()
    {
        var bird = new Bird(_config.StartPosition, _config.StartRotation, _config.Scale, _config.JumpVelocity);
        _inputService.BindAction(bird.Jump, _config.JumpButton);

        return bird;
    }
}