using System;

public class BirdBullet : Bullet, IDamageable   
{
    public event Action<BirdBullet> Dead;

    public void TakeDamage()
    {
        Dead?.Invoke(this);
    }
}