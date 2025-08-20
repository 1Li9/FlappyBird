using System;

public interface IEntity : ITransformable, IDisableable, IEnableable
{
    public event Action Enabled;
    public event Action Disabled;
}