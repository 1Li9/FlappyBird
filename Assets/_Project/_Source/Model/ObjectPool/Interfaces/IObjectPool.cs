public interface IObjectPool<T>  where T : class
{
    public T Get();
    public void Release(T obj);
    public void Clear();
}
