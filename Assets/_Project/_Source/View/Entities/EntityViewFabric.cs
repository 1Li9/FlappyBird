using UnityEngine;

public class EntityViewFabric : MonoBehaviour
{
    [SerializeField] private EntityPrefabs _entityPrefabs;
    [SerializeField] private CollisionProcessorCompositeRoot _collisionProcessor;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;

    private EntityAnimator _animator;

    public EntityView Create(IEntity model)
    {
        _animator = null;

        EntityView prefab = _entityPrefabs.GetPrefab(model);

        EntityView view = Instantiate(prefab);
        view.transform.SetLocalPositionAndRotation(model.Position, model.Rotation);
        view.transform.localScale = model.Scale;

        view.Init(model);

        if (view.gameObject.TryGetComponent(out ColliderBroadcaster broadcaster))
            broadcaster.Init(_collisionProcessor.Model, model);

        if (view.gameObject.TryGetComponent(out _animator))
        {
            _animator.Init();
            _servicesRoot.Stop.Add(_animator);
            _servicesRoot.Pause.Add(_animator);
        }

        return view;
    }

    public EntityAnimator Animator => _animator;
}