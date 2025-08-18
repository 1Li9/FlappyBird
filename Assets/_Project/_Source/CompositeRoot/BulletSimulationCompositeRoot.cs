using UnityEngine;

public class BulletSimulationCompositeRoot : CompositeRoot
{
    [SerializeField] private WeaponConfig _weaponConfig;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;

    public MoveSimulation Simulation { get; private set; }

    private void Update()
    {
        Simulation.Tick(Time.deltaTime);
    }

    public override void Composite()
    {
        Simulation = new MoveSimulation(Vector3.left, _weaponConfig.BulletSpeed);
        _servicesRoot.Pause.Add(Simulation);
        _servicesRoot.Stop.Add(Simulation);
    }
}