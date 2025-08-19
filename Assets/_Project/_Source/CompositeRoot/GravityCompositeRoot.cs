using UnityEngine;
public class GravityCompositeRoot : CompositeRoot
{
    [SerializeField] private PhysicsConfig _config;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;

    public GravitySimulation Simulation { get; private set; }

    private void OnDisable()
    {
        _servicesRoot.Dispose.Remove(Simulation);
        _servicesRoot.Tick.Remove(Simulation);
    }

    public override void Composite()
    {
        Simulation = new GravitySimulation(_config);
        _servicesRoot.Dispose.Add(Simulation);
        _servicesRoot.Tick.Add(Simulation);
    }
}