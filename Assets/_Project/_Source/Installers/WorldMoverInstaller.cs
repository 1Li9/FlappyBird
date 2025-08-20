using UnityEngine;

public class WorldMoverInstaller : Installer 
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private ServicesInstaller _servicesInstaller;

    public MoveSimulation<IPositional> Simulation { get; private set; }

    private void OnValidate()
    {
        _direction.Normalize();
    }

    private void OnDisable()
    {
        _servicesInstaller.Dispose.Remove(Simulation);
        _servicesInstaller.UpdateSevice.Remove(Simulation);
    }

    public override void Install()
    {
        Simulation = new MoveSimulation<IPositional>(_direction, _speed);
        _servicesInstaller.Dispose.Add(Simulation);
        _servicesInstaller.UpdateSevice.Add(Simulation);
    }
}