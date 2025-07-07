public class BirdFabric
{
    private readonly IBirdConfig _config;
    private readonly Bird _bird;

    public BirdFabric(IBirdConfig config)
    {
        _config = config;

        if(config == null)
            throw new System.ArgumentNullException(nameof(config));

        _bird = new Bird(_config.StartVelocity, _config.StartPosition, _config.StartRotation, _config.Scale, _config.JumpVelocity);
    }

    public Bird Bird => _bird;
}