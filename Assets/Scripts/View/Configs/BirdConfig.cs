using UnityEngine;

[CreateAssetMenu(fileName ="Bird", menuName = "NewBird", order = 51)]
public class BirdConfig : ScriptableObject, IBirdConfig
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Vector3 _startVelocity;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private PhysicalObjectView _view;

    public Vector3 StartPosition => _startPosition;
    public Vector3 StartRotation => _startRotation;
    public Vector3 StartVelocity => _startVelocity;
    public Vector3 Scale => _scale;
    public float JumpVelocity => _jumpVelocity;
    public PhysicalObjectView View => _view;
}