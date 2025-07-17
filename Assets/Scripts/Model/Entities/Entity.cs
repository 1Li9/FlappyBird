using UnityEngine;
using System;

public class Entity : ITransformable, IEnableable, IDisposable
{
    public Entity(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;

        IsActive = true;
    }

    public bool IsActive { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Vector3 Scale { get; private set; }

    public event Action Disposed;
    public event Action Enabled;

    public void Dispose()
    {
        Disposed?.Invoke();
        IsActive = false;
    }

    public void Enable()
    {
        Enabled?.Invoke();
        IsActive = true;
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
    }

    public void SetRotation(Vector3 rotation)
    {
        Rotation = rotation;
    }

    public void SetScale(Vector3 scale)
    {
        Scale = scale;
    }
}