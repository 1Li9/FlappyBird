using UnityEngine;

public class EnemySpawnStrategy : ISpawnStrategy<Enemy>
{
    private readonly MoveSimulation _mover;
    private readonly Vector3 _spawnPosition;
    private readonly float _height;

    public EnemySpawnStrategy(MoveSimulation mover, Vector3 spawnPosition, float height)
    {
        _mover = mover;
        _spawnPosition = spawnPosition;
        _height = height;
    }

    public void Spawn(Enemy enemy)
    {
        if(_mover.Contains(enemy) == false)
            _mover.Add(enemy);

        float max = _height / 2;
        float min = -max;
        float yPosition = Random.Range(min, max);

        Vector3 position = _spawnPosition;
        position.y = yPosition;

        enemy.SetPosition(position);
    }
}