using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private float _spawnTime;

    private Spawner<Entity> _spawner;
    private ObjectPool<Entity> _pool;
    private ObjectPoolReleaser<Entity> _releaser;
    private WorldMoverSimulation _mover;

    public void Initialise(ObjectPool<Entity> pool, WorldMoverSimulation mover)
    {
        _pool = pool;
        _mover = mover; 
        _spawner = new Spawner<Entity>(_pool, Vector2.one, 2, _spawnTime);
    }

    private void Update()
    {
        _spawner.Spawn(Time.deltaTime);
    }
}
