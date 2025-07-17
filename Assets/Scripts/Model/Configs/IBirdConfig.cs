using UnityEngine;

public interface IBirdConfig
{
    public Vector3 StartPosition { get; }
    public Vector3 StartRotation { get; }
    public Vector3 StartVelocity { get; }
    public Vector3 Scale { get; }
    public float JumpVelocity { get; }
    public IInputService Input { get; }
    public IInputInformation JumpButton { get; }
}