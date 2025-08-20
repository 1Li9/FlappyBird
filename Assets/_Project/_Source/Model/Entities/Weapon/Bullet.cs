using System;
using UnityEngine;

public class Bullet : IEntity, IDamageable
{
    public Vector3 Position { get; private set; }

    public Quaternion Rotation { get; private set; }

    public Vector3 Scale { get; private set; }

    public Vector3 Direction { get; private set; }

    public event Action Enabled;
    public event Action Disabled;
    public Action<Bullet> Dead;

    public void Disable()
    {
        Disabled?.Invoke();
    }

    public void Enable()
    {
        Enabled?.Invoke();
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        Rotation = rotation;
    }

    public void SetScale(Vector3 scale)
    {
        Scale = scale;
    }

    public void SetDirection(Vector3 direction)
    {
        direction.Normalize();
        Direction = direction;
    }

    public void TakeDamage()
    {
        Dead?.Invoke(this);
    }
}