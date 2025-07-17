using UnityEngine;

public class Spawner<T> where T : class, IEnableable, System.IDisposable, ITransformable
{
    private readonly ObjectPool<T> _pool;
    private readonly Vector3 _position;
    private readonly float _sphereRadius;
    private readonly float _spawnTime;

    private  float _currentTime;

    public Spawner(ObjectPool<T> objectPool, Vector3 position, float sphereRadius, float spawnTime)
    {
        _position = position;
        _sphereRadius = sphereRadius;
        _pool = objectPool;
        _spawnTime = spawnTime;
    }

    public void Spawn(float deltaTime)
    {
        if (_currentTime > 0)
        {
            _currentTime -= deltaTime;
            return;
        }

        _currentTime = _spawnTime;

        if (_pool.TryGet(out T obj))
        {
            Vector3 insideUnitCircle = Random.insideUnitCircle;
            Vector3 position = new Vector3(insideUnitCircle.x, insideUnitCircle.y, 0) * _sphereRadius + _position;
            obj.SetPosition(position);
        }
    }
}