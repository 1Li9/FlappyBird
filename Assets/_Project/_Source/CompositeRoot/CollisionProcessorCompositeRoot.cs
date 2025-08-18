using UnityEngine;

public class CollisionProcessorCompositeRoot : CompositeRoot
{
    [SerializeField] private ServicesCompositeRoot _servicesRoot;

    public CollisionProcessor Model { get; private set; }
    public CollisonRecords Records { get; private set; }

    public override void Composite()
    {
        Records = new CollisonRecords(_servicesRoot.Stop);
        Model = new CollisionProcessor(Records.Get());
    }
}