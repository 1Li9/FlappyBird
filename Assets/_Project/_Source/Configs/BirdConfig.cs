using UnityEngine;

[CreateAssetMenu(fileName = "BirdConfig", menuName = "CreateBirdConfig", order = 51)]
public class BirdConfig : ScriptableObject
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _maxRotationAngle;
    [SerializeField] private float _minRotationAngle;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _startPosition;

    public float JumpForce => _jumpForce;

    public float MaxRotationAngle => _maxRotationAngle;

    public float MinRotationAngle => _minRotationAngle;

    public float RotationSpeed => _rotationSpeed;

    public Vector3 StartPosition => _startPosition;
}