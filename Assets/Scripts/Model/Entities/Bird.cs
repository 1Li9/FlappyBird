using UnityEngine;

public class Bird : PhysicalEntity
{
    private float _jumpVelocity;

    public Bird(Vector3 position, Vector3 rotation, Vector3 scale, float jumpVelocity) : base(position, rotation, scale)
    {
        _jumpVelocity = jumpVelocity;
    }

    public void Jump()
    {
        if (Velocity.y < 0)
            SetVelocity(new Vector3(Velocity.x, _jumpVelocity, Velocity.z));
        else
            SetVelocity(new Vector3(0, _jumpVelocity, 0));
    }
}