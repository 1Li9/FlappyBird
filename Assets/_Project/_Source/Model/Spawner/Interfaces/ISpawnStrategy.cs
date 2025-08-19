using System;
using UnityEngine;

public interface ISpawnStrategy<T> where T : class, ITransformable
{
    public void Spawn(Func<Vector3, T> getObj);
}