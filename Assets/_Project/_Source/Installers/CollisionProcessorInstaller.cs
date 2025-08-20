using UnityEngine;

public class CollisionProcessorInstaller : Installer
{
    [SerializeField] private ServicesInstaller _services;

    public CollisionProcessor Model { get; private set; }
    public CollisonRecords Records { get; private set; }

    private void OnDisable()
    {
        _services.UpdateSevice.Remove(Model);
    }

    public override void Install()
    {
        Records = new CollisonRecords();
        Model = new CollisionProcessor(Records.Get());
        _services.UpdateSevice.Add(Model);
    }
}