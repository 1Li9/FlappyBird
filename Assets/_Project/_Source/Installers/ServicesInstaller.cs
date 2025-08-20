public class ServicesInstaller : Installer
{
    public PauseService Pause { get; private set; }  
    public DisposeService Dispose { get; private set; }
    public TimerService Timers { get; private set; }
    public UpdateService UpdateSevice { get; private set; }

    public override void Install()
    {
        Pause = new PauseService();
        Dispose = new DisposeService();
        Timers = new TimerService();
        UpdateSevice = new UpdateService();

        Dispose.Add(Timers);
        Pause.Add(UpdateSevice);
        Dispose.Add(UpdateSevice);
        UpdateSevice.Add(Timers);
    }

    private void OnDisable()
    {
        Dispose.Remove(Timers);
        Pause.Remove(UpdateSevice);
        Dispose.Remove(UpdateSevice);
        UpdateSevice.Remove(Timers);
    }

    private void Update()
    {
        UpdateSevice.Tick(UnityEngine.Time.deltaTime);
        UpdateSevice.Update();
    }
}