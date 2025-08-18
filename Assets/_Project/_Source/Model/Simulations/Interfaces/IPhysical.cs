using UnityEngine;

public interface IPhysical : IEntity
{
    public Vector3 Velocity { get; }

    public void SetVelocity(Vector3 speed);
}