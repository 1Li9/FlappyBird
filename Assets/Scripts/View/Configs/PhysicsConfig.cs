using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsConfig", menuName = "NewPhysicsConfig", order = 51)]
public class PhysicsConfig : ScriptableObject, IPhysicsConfig
{
    [SerializeField] private float _gravityAccelerationScale;
    [SerializeField] private float _maxVerticalFlySpeed;
    [SerializeField] private float _maxFallSpeed;

    public float GravityAccelerationScale => _gravityAccelerationScale;
    public float MaxVerticalFlySpeed => _maxVerticalFlySpeed;
    public float MaxFallSpeed => _maxFallSpeed;
}
