using System;
using UnityEngine;

public class BirdJumper
{
    private readonly Bird _bird;
    private readonly float _force;

    public event Action Jumped;

    public BirdJumper(Bird bird, float force)
    {
        _bird = bird;
        _force = force;
    }

    public void Jump()
    {
        Vector3 velocity;

        if (_bird.Velocity.y > 0)
            velocity = _bird.Velocity + new Vector3(0, _force, 0);
        else
            velocity = new Vector3(_bird.Velocity.x, _force, _bird.Velocity.z);

        _bird.SetVelocity(velocity);
        Jumped?.Invoke();
    }
}