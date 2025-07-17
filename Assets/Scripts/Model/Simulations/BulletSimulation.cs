using System.Collections.Generic;
using UnityEngine;

public class BulletSimulation : ISimulation
{
    private List<Bullet> _bullets;
    private float _speed;

    public BulletSimulation(float speed)
    {
        _bullets = new List<Bullet>();
        _speed = speed;
    }

    public void Simulate(float deltaTime)
    {
        foreach (Bullet bullet in _bullets)
        {
            Vector3 newPosition = bullet.Position + new Vector3(-_speed, 0, 0) * deltaTime;
            bullet.SetPosition(newPosition);
        }
    }

    public void Add(Bullet bullet)
    {
        _bullets.Add(bullet);
    }

    public void Remove(Bullet bullet)
    {
        _bullets.Remove(bullet);
    }
}