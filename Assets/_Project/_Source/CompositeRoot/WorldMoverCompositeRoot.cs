using UnityEngine;

public class WorldMoverCompositeRoot : CompositeRoot 
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;

    public MoveSimulation Simulation { get; private set; }

    private void OnValidate()
    {
        _direction.Normalize();
    }

    private void Update()
    {
        Simulation.Tick(Time.deltaTime);
    }

    private void OnDisable()
    {
        Simulation.Dispose();
    }

    public override void Composite()
    {
        Simulation = new MoveSimulation(_direction, _speed);
        _servicesRoot.Pause.Add(Simulation);
        _servicesRoot.Stop.Add(Simulation);
    }
}