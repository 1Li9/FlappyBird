using System;

public interface ISpawnEventPublisher
{
    public event Action Spawning;
}