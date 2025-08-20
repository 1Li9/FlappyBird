using System;
using UnityEngine;

public class BulletFabric
{
    public Bullet Create(Func<Bullet> model, Vector3 position)
    {
        Bullet bullet = model();
        bullet.SetPosition(position);
        bullet.SetScale(Vector3.one);

        return bullet;
    }
}
