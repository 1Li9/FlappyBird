public class EntityReleaser<T> where T : class, IEntity
{
    private readonly IObjectPool<T> _pool;

    public EntityReleaser(IObjectPool<T> pool)
    {
        _pool = pool;
    }

    public void Release(T entity) =>
        _pool.Release(entity);
}