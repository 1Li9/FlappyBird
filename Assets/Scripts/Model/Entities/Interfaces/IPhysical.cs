using UnityEngine;

public interface IPhysical : IPositionable
{
    public Vector3 Velocity { get; }
    public void SetVelocity(Vector3 velocity);
}
