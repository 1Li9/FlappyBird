using System;

public interface IEntity : ITransformable, IDisableable, IEnableable
{
    public event Action OnEnable;
    public event Action OnDisable;
}