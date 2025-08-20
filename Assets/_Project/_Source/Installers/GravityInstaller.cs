using UnityEngine;
public class GravityInstaller : Installer
{
    [SerializeField] private PhysicsConfig _config;
    [SerializeField] private ServicesInstaller _servicesInstaller;

    public GravitySimulation Simulation { get; private set; }

    private void OnDisable()
    {
        _servicesInstaller.Dispose.Remove(Simulation);
        _servicesInstaller.UpdateSevice.Remove(Simulation);
    }

    public override void Install()
    {
        Simulation = new GravitySimulation(_config);
        _servicesInstaller.Dispose.Add(Simulation);
        _servicesInstaller.UpdateSevice.Add(Simulation);
    }
}