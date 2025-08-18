using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EntityReleaser<T> : MonoBehaviour where T : class, IEntity
{
    private IObjectPool<T> _entityPool;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityView view) && view.Model is T model)
            _entityPool.Release(model);
    }

    public void BindPool(IObjectPool<T> entityPool)
    {
        _entityPool = entityPool;
    }
}