using UnityEngine;

public class BirdRotator : ITickable
{
    private readonly float _maxAngle;
    private readonly float _minAngle;
    private readonly float _speed;

    private readonly Bird _bird;

    public BirdRotator(float maxAngle, float minAngle, float speed, Bird bird)
    {
        _maxAngle = maxAngle;
        _minAngle = minAngle;
        _speed = speed;
        _bird = bird;
    }


    public void Tick(float deltaTime)
    {
        Quaternion minRotation = Quaternion.Euler(0, 0, -_minAngle);
        Quaternion rotation = Quaternion.Lerp(_bird.Rotation, minRotation, _speed * deltaTime);
        _bird.SetRotation(rotation);
    }

    public void OnJump()
    {
        Vector3 angle = new(0, 0, _maxAngle);
        _bird.SetRotation(Quaternion.Euler(angle));
    }
}
