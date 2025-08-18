using UnityEngine;
public class GravityCompositeRoot : CompositeRoot
{
    [SerializeField] private PhysicsConfig _config;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;

    public GravitySimulation Simulation { get; private set; }

    public override void Composite()
    {
        Simulation = new GravitySimulation(_config);
        _servicesRoot.Pause.Add(Simulation);
        _servicesRoot.Stop.Add(Simulation);
    }

    private void LateUpdate()
    {
        Simulation.Tick(Time.deltaTime);
    }
}