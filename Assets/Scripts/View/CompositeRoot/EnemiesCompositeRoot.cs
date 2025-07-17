using System.Collections.Generic;
using UnityEngine;

public class EnemiesCompositeRoot : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private int _speed;
    [SerializeField] private SpawnerView _spawnerView;
    [SerializeField] private EntityView _view;
    [SerializeField] private WeaponConfig _weaponConfig;

    private ObjectPool<Entity> _pool;
    private Entity[] _enemies;
    private List<EntityView> _entitiesViews;
    private EnemyFabric _fabric;

    public void Initialize(WorldMoverSimulation mover)
    {
        _entitiesViews = new List<EntityView>();
        BulletSimulation bulletSimulation = new(5f);
        _fabric = new EnemyFabric(_count, _weaponConfig, mover, bulletSimulation);

        _enemies = _fabric.Create();
        _pool = new ObjectPool<Entity>(_enemies);

        foreach (Entity entity in _enemies)
        {
            EntityView view = Instantiate(_view);
            view.Bind(entity);
            _entitiesViews.Add(view);
        }

        _spawnerView.Initialise(_pool, mover);
    }

    public EntityView[] GetEntitiesViews() =>
        _entitiesViews.ToArray() ?? throw new System.InvalidOperationException(nameof(GetEntitiesViews));
}