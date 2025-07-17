using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EntityView : MonoBehaviour
{
    private Collider _collider;

    public Entity Model {get; private set;}

    private void Awake()
    {
        _collider= GetComponent<Collider>();
    }

    private void Update()
    {
        transform.position = Model.Position;
        transform.rotation = Quaternion.Euler(Model.Rotation);
    }

    private void OnEnable()
    {
        if (Model == null)
            return;

        Model.Enabled += Enable;
        Model.Disposed += Dispose;
    }

    private void OnDisable()
    {
        Model.Enabled -= Dispose;
        Model.Disposed -= Dispose;
    }

    public void Bind(Entity model)
    {
        Model = model;

        if(Model.IsActive == false)
            Dispose();

        Model.Enabled += Enable;
        Model.Disposed += Dispose;
    }

    public Bounds GetBounds() =>
        _collider.bounds;

    private void Enable()
    {
        gameObject.SetActive(true);
    }

    private void Dispose()
    {
        gameObject.SetActive(false);
    }
}
