using System.Collections.Generic;
using UnityEngine;

public class GameCompositeRoot : MonoBehaviour
{
    [SerializeField] private ColliderCompositeRoot _colliderCompositeRoot;
    [SerializeField] private BirdCompositeRoot _birdCompositeRoot;
    [SerializeField] private EnemiesCompositeRoot _enemiesCompositeRoot;
    [SerializeField] private GameView _view;

    [SerializeField] private PhysicsConfig _physicsConfig;

    [SerializeField] private float _worldSpeed;

    private void Start()
    {
        EntityView birdView = _birdCompositeRoot.CreateBirdView();
        var simulation = new PhysicsSimulation(_physicsConfig);

        if(birdView.Model is IPhysical physical)
            simulation.Add(physical);

        WorldMoverSimulation mover = new(Vector3.left, _worldSpeed);
        _enemiesCompositeRoot.Initialize(mover);

        List<EntityView> views = new() { birdView };
        views.AddRange(_enemiesCompositeRoot.GetEntitiesViews());

        _colliderCompositeRoot.Initialize(simulation, views.ToArray());

        GameFabric fabric = new(simulation, mover);

        _view.Initialize(fabric.Create(), _colliderCompositeRoot.CollisionRouter);
    }
}