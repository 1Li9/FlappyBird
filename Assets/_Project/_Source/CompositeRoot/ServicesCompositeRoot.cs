public class ServicesCompositeRoot : CompositeRoot
{
    public PauseService Pause { get; private set; }  
    public DisposeService Dispose { get; private set; }
    public TimerService Timers { get; private set; }
    public TickService Tick { get; private set; }

    public override void Composite()
    {
        Pause = new PauseService();
        Dispose = new DisposeService();
        Timers = new TimerService();
        Tick = new TickService();

        Dispose.Add(Timers);
        Pause.Add(Tick);
        Dispose.Add(Tick);
        Tick.Add(Timers);
    }

    private void OnDisable()
    {
        Dispose.Remove(Timers);
        Pause.Remove(Tick);
        Dispose.Remove(Tick);
        Tick.Remove(Timers);
    }

    private void Update()
    {
        Tick.Tick(UnityEngine.Time.deltaTime);
    }
}