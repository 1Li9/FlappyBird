using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhysicalEntityView : MonoBehaviour
{
    private Collider _collider;
    private PhysicalEntity _model;

    public IPhysical Model => _model;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        if (_model == null)
            return;

        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Update()
    {
        transform.SetPositionAndRotation(_model.Position, Quaternion.Euler(_model.Rotation));
    }

    public void Bind(PhysicalEntity model)
    {
        _model = model;
        Subscribe();
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

    private void Unsubscribe()
    {
        _model.Enabled -= Enable;
        _model.Disposed -= Dispose;
    }

    private void Subscribe()
    {
        _model.Enabled += Enable;
        _model.Disposed += Dispose;
    }
}