using System;
using UnityEngine;

public class EntityPrefab : MonoBehaviour
{
    [SerializeField] private EntityView _birdView;
    [SerializeField] private EntityView _enemyView;
    [SerializeField] private EntityView _bulletView;

    public EntityView GetPrefab(IEntity entity)
    {
        if(entity is Bird)
            return _birdView;
        else if(entity is Enemy)
            return _enemyView;
        else if(entity is Bullet) 
            return _bulletView;
        
        throw new InvalidOperationException(nameof(GetPrefab));
    }
}