using System.Collections.Generic;
using UnityEngine;

public class CollisionRouter : MonoBehaviour
{
    private PhysicsSimulation _physicsSimulation;
    private EntityView[] _views;

    public void Initialize(params EntityView[] views)
    {
        _views = views;
    }

    public void Bind(PhysicsSimulation simulation)
    {
        _physicsSimulation = simulation;
    }

    public void ProcessCollisions()
    {
        for (int i = 0; i < _views.Length; i++)
        {
            for (int j = i + 1; j < _views.Length; j++)
            {
                EntityView aObj = _views[i];
                EntityView bObj = _views[j];

                if (aObj.gameObject.activeSelf == false | bObj.gameObject.activeSelf == false)
                    continue;

                if (aObj.GetBounds().Intersects(bObj.GetBounds()))
                    _physicsSimulation.ResolveCollision(aObj.Model, bObj.Model);
            }
        }
    }
}