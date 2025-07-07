using System.Collections.Generic;
using UnityEngine;

public class PhysicsCompositeRoot : MonoBehaviour
{
    [SerializeField] private PhysicsSimulationView _view;
    [SerializeField] private BirdConfig _birdConfig;

    private Dictionary<IPhysical, PhysicalObjectView> _instantiatedPhysicalObjects;
    private PhysicsSimulation _simulation;

    private void Awake()
    {
        _instantiatedPhysicalObjects = new Dictionary<IPhysical, PhysicalObjectView>();
        _simulation = new PhysicsSimulation();
    }

    private void Start()
    {
        CreateBirdView();
        _view.Bind(_simulation);
    }

    private void CreateBirdView()
    {
        BirdFabric birdFabric = new(_birdConfig);
        PhysicalObjectView birdView = Instantiate(_birdConfig.View);

        birdView.Bind(birdFabric.Bird);

        _instantiatedPhysicalObjects.Add(birdView.Model, birdView);
        _simulation.Add(birdView.Model);
    }
}