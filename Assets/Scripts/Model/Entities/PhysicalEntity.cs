using UnityEngine;

public class PhysicalEntity : Entity, IPhysical
{
    public PhysicalEntity(Vector3 position, Vector3 rotation, Vector3 scale) : base(position, rotation, scale)
    {
        Velocity = Vector3.zero;
    }

    public Vector3 Velocity { get; private set; }

    public void SetVelocity(Vector3 velocity)
    {
        Velocity = velocity;
    }
}