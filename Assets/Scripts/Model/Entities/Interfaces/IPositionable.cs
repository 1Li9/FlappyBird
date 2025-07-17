using UnityEngine;

public interface IPositionable
{
    public Vector3 Position { get; }

    public void SetPosition(Vector3 position);
}