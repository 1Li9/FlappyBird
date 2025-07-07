using UnityEngine;

public class Bird : Enemy, IPhysical
{
    private float _jumpVelocity;

    public Bird(Vector3 velocity, Vector3 position, Vector3 rotation, Vector3 scale, float jumpVelocity) : base(position, rotation, scale)
    {
        Velocity = velocity;
        _jumpVelocity = jumpVelocity;
    }

    public Vector3 Velocity { get; private set; }

    public void Jump()
    {
        Velocity += new Vector3(0, _jumpVelocity, 0);
    }

    public void SetVelocity(Vector3 velocity) =>
        Velocity = velocity;
}