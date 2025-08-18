public class ServicesCompositeRoot : CompositeRoot
{
    public PauseService Pause { get; private set; }  
    public StopService Stop { get; private set; }

    public override void Composite()
    {
        Pause = new PauseService();
        Stop = new StopService();
    }
}