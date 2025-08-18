using UnityEngine;

public interface ITransformable : IPositional
{
    public Quaternion Rotation { get; }
    public Vector3 Scale { get; }

    public void SetRotation(Quaternion rotation);
    public void SetScale(Vector3 scale);
}