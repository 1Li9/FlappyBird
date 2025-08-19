using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EntityReleaserView<T> : MonoBehaviour where T : class, IEntity
{
    private EntityReleaser<T> _model;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityView view) && view.Model is T model)
            _model.Release(model);
    }

    public void Bind(EntityReleaser<T> model)
    {
        _model = model;
    }
}