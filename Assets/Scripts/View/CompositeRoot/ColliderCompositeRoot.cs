using UnityEngine;

public class ColliderCompositeRoot : MonoBehaviour
{
    [SerializeField] private CollisionRouter _router;

    public CollisionRouter CollisionRouter => _router;

    public void Initialize(PhysicsSimulation simulation, params EntityView[] entities)
    {
        _router.Initialize(entities);
        _router.Bind(simulation);
    }
}