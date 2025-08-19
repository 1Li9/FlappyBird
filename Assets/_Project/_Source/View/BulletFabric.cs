using System;
using UnityEngine;

public class BulletFabric
{
    public T Create<T>(Func<T> model, Vector3 position) where T : Bullet
    {
        T bullet = model();
        bullet.SetPosition(position);
        bullet.SetScale(Vector3.one);

        return bullet;
    }
}
