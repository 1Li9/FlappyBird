using System.Collections.Generic;
using UnityEngine;

public class BulletSimulation<T> : ISimulation<T> where T : Bullet
{
    private readonly List<T> _bullets = new();
    private readonly float _speed;

    public BulletSimulation(float speed)
    {
        _speed = speed;
    }

    public void Add(T item)
    {
        _bullets.Add(item);
    }

    public bool Contains(T item)
    {
        return _bullets.Contains(item);
    }

    public void Dispose()
    {
        _bullets.Clear();
    }

    public void Remove(T item)
    {
        _bullets.Remove(item);
    }

    public void Tick(float deltaTime)
    {
        foreach (T bullet in _bullets)
        {
            Vector3 position = bullet.Position;
            Vector3 newPosition = position + bullet.Direction;
            bullet.SetPosition(Vector3.MoveTowards(position, newPosition, _speed * deltaTime));
        }
    }
}