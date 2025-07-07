using UnityEngine;

public interface IPhysical : ITransformable
{
    public Vector3 Velocity { get; }
    public void SetVelocity(Vector3 velocity);
}
