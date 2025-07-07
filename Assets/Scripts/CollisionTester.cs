using UnityEngine;

public class CollisionTester : MonoBehaviour
{
    public Collider colliderA;
    public Collider colliderB;

    void Update()
    {
        // === 1. Быстрая, но неточная проверка (AABB) ===
        bool aabbIntersect = colliderA.bounds.Intersects(colliderB.bounds);

        if(aabbIntersect )
            Debug.Log($"AABB intersects: {aabbIntersect}");

        // === 2. Точная проверка столкновения ===
        bool preciseCollision = Physics.ComputePenetration(
            colliderA, colliderA.transform.position, colliderA.transform.rotation,
            colliderB, colliderB.transform.position, colliderB.transform.rotation,
            out Vector3 direction, out float distance
        );

        if (preciseCollision)
        {
            Debug.Log($"Точное столкновение: {colliderA.name} vs {colliderB.name}, " +
                      $"направление: {direction}, глубина: {distance}");
        }
    }
}
