using UnityEngine;

public class WorldMoverCompositeRoot : CompositeRoot 
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;

    public MoveSimulation<IPositional> Simulation { get; private set; }

    private void OnValidate()
    {
        _direction.Normalize();
    }

    private void OnDisable()
    {
        _servicesRoot.Dispose.Remove(Simulation);
        _servicesRoot.Tick.Remove(Simulation);
    }

    public override void Composite()
    {
        Simulation = new MoveSimulation<IPositional>(_direction, _speed);
        _servicesRoot.Dispose.Add(Simulation);
        _servicesRoot.Tick.Add(Simulation);
    }
}