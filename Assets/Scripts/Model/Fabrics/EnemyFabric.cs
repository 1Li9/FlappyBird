using UnityEngine;

public class EnemyFabric
{
    private readonly WorldMoverSimulation _mover;
    private readonly BulletSimulation _bulletSimulation;
    private readonly Weapon _weapon;

    public EnemyFabric(Weapon weapon, WorldMoverSimulation mover, BulletSimulation bulletSimulation)
    {
        _mover = mover;
        _bulletSimulation = bulletSimulation;
        _weapon = weapon;
    }

    public Enemy Create()
    {
        Enemy enemy = new(Vector3.zero, Vector3.zero, Vector3.one);
        enemy.BindWeapon(_weapon);
        _mover.Add(enemy);

        return enemy;
    }
}