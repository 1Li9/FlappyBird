using UnityEngine;

public interface IPositional
{
    public Vector3 Position { get; }
    public void SetPosition(Vector3 position);
}