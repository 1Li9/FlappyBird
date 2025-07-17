using UnityEngine;

public class Enemy : Entity, ITickable
{
    private Weapon _weapon;

    public Enemy(Vector3 position, Vector3 rotation, Vector3 scale) : base(position, rotation, scale)
    {
    }

    public void BindWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void Tick(float deltaTime)
    {
        _weapon.Tick(deltaTime);
    }
}