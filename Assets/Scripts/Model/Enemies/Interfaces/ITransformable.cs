using UnityEngine;

public interface ITransformable
{
    public Vector3 Position { get; }
    public Vector3 Rotation { get; }
    public Vector3 Scale { get; }

    public void SetPosition(Vector3 position);
    public void SetRotation(Vector3 rotation);
    public void SetScale(Vector3 scale);
}
