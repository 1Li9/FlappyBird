public class CollisionProcessorCompositeRoot : CompositeRoot
{
    public CollisionProcessor Model { get; private set; }
    public CollisonRecords Records { get; private set; }

    private void Update()
    {
        Model.Process();
    }

    public override void Composite()
    {
        Records = new CollisonRecords();
        Model = new CollisionProcessor(Records.Get());
    }
}   