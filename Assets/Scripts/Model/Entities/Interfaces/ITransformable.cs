using UnityEngine;

public interface ITransformable : IPositionable
{
    public Vector3 Rotation { get; }
    public Vector3 Scale { get; }

    public void SetRotation(Vector3 rotation);
    public void SetScale(Vector3 scale);
}