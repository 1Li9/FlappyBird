public class InputCompositeRoot : CompositeRoot
{
    public GameControlls Inputs { get; private set; }

    private void OnDisable()
    {
        Inputs.Disable();
    }

    public override void Composite()
    {
        Inputs = new GameControlls();
        Inputs.Enable();
    }
}