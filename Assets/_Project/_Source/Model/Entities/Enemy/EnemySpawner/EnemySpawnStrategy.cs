using System;
using UnityEngine;

public class EnemySpawnStrategy : ISpawnStrategy<Enemy>
{
    private readonly MoveSimulation<IPositional> _mover;
    private readonly Vector3 _spawnPosition;
    private readonly float _height;

    public EnemySpawnStrategy(MoveSimulation<IPositional> mover, Vector3 spawnPosition, float height)
    {
        _mover = mover;
        _spawnPosition = spawnPosition;
        _height = height;
    }

    public void Spawn(Func<Vector3, Enemy> getEnemy)
    {
        float max = _height / 2;
        float min = -max;
        float yPosition = UnityEngine.Random.Range(min, max);

        Vector3 position = _spawnPosition;
        position.y = yPosition;

        Enemy enemy = getEnemy(position);

        if(_mover.Contains(enemy) == false)
            _mover.Add(enemy);

        enemy.SetPosition(position);
    }
}