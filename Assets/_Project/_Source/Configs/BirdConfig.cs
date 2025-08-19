using UnityEngine;

[CreateAssetMenu(fileName = "BirdConfig", menuName = "CreateBirdConfig", order = 51)]
public class BirdConfig : ScriptableObject
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _maxRotationAngle;
    [SerializeField] private float _minRotationAngle;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _startPosition;

    [SerializeField] private Vector3 _bulletDirection;
    [SerializeField] private float _buletSpeed;
    [SerializeField] private float _bulletXSpawnGap;

    public float JumpForce => _jumpForce;
    public float MaxRotationAngle => _maxRotationAngle;
    public float MinRotationAngle => _minRotationAngle;
    public float RotationSpeed => _rotationSpeed;
    public Vector3 StartPosition => _startPosition;

    public Vector3 BulletDirection => _bulletDirection;
    public float BuletSpeed => _buletSpeed;
    public float BulletXSpawnGap => _bulletXSpawnGap;

    private void OnValidate()
    {
        _bulletDirection.Normalize();
    }
}