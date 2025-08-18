using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsConfig", menuName = "CreatePhysicsConfig", order = 51)]
public class PhysicsConfig : ScriptableObject, IGravityConfig
{
    [SerializeField] private float _gravityAccelerationScale;
    [SerializeField] private float _maxFlySpeed;
    [SerializeField] private float _maxFalllSpeed;

    public float GravityAccelerationScale => _gravityAccelerationScale;

    public float MaxFlySpeed => _maxFlySpeed;

    public float MaxFalllSpeed => _maxFalllSpeed;
}