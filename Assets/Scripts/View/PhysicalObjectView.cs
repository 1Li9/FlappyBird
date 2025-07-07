using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhysicalObjectView : MonoBehaviour
{
    protected Collider Ñollider;

    public IPhysical Model { get; private set; }

    private void Awake()
    {
        Ñollider = GetComponent<Collider>();
    }

    private void Update()
    {
        transform.position = Model.Position;
        transform.rotation = Quaternion.Euler(Model.Rotation);
    }

    public void Bind(IPhysical model) =>
        Model = model;

    public Bounds GetBounds() => 
        Ñollider.bounds;
}