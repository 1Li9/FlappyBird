using System;
using UnityEngine;

public class Enemy : IEntity, IDamageable
{
    public Vector3 Position { get; private set; }

    public Quaternion Rotation { get; private set; }

    public Vector3 Scale { get; private set; }

    public event Action OnEnable;
    public event Action OnDisable;
    public event Action<Enemy> Dead;

    public void Disable()
    {
        OnDisable?.Invoke();
    }

    public void Enable()
    {
        OnEnable?.Invoke();
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

    public void TakeDamage()
    {
        Dead?.Invoke(this);
    }
}