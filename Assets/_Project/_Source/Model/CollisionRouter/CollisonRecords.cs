using System;
using System.Collections.Generic;

public class CollisonRecords
{
    public event Action GameStopped;

    public IEnumerable<IRecord> Get()
    {
        yield return GetRecord((Bird _, EnemyBullet _) =>
        {
            GameStopped?.Invoke();
        });

        yield return GetRecord((Enemy enemy, BirdBullet bullet) =>
        {
            enemy.TakeDamage();
            bullet.TakeDamage();
        });
    }

    private Record<T1, T2> GetRecord<T1, T2>(Action<T1, T2> action)
    {
        return new Record<T1, T2>(action);
    }
}