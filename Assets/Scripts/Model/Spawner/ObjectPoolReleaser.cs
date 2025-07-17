using System;

public class ObjectPoolReleaser<T> where T : class, IEnableable, IDisposable, ITransformable
{
    private readonly ObjectPool<T> _objectPool;
    private readonly Action<T> _callBack;   
    private readonly Func<float, bool> _releaseCondition;

    public ObjectPoolReleaser(ObjectPool<T> objectPool, Func<float, bool> releaseCondition, Action<T> callBack)
    {
        _objectPool = objectPool;
        _releaseCondition = releaseCondition;
        _callBack = callBack;
    }

    public void Update()
    {
        foreach (T obj in _objectPool.ActiveObjs)
        {
            if (_releaseCondition.Invoke(obj.Position.x))
            {
                _objectPool.Release(obj);
                _callBack?.Invoke(obj);

                return;
            }
        }
    }
}