using UnityEngine;

public class Enemy : ITransformable
{
    public Enemy(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    public Vector3 Position { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Vector3 Scale { get; private set; }

    public void SetPosition(Vector3 position)
    {
       Position= position;
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