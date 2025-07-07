using System.Collections.Generic;
using UnityEngine;

public class PhysicsSimulationView : MonoBehaviour
{
    private PhysicsSimulation _physicsSimulation;
    private Dictionary<IPhysical, PhysicalObjectView> _instantiatedPhysicalObjects;

    private void LateUpdate()
    {
        ProcessCollisions();
        _physicsSimulation.Simulate(Time.deltaTime);
    }

    public void Initialize(Dictionary<IPhysical, PhysicalObjectView> instantiatedObjects)
    {
        _instantiatedPhysicalObjects = instantiatedObjects;
    }

    public void Bind(PhysicsSimulation simulation)
    {
        _physicsSimulation = simulation;
    }

    private void ProcessCollisions()
    {
        for (int i = 0; i < _physicsSimulation.Objects.Count; i++)
        {
            for (int j = i + 1; j < _physicsSimulation.Objects.Count; j++)
            {
                IPhysical aObj = _physicsSimulation.Objects[i];
                IPhysical bObj = _physicsSimulation.Objects[j];

                PhysicalObjectView aView = _instantiatedPhysicalObjects[aObj];
                PhysicalObjectView bView = _instantiatedPhysicalObjects[bObj];

                if (aView.GetBounds().Intersects(bView.GetBounds()))
                    _physicsSimulation.ResolveCollision(aView.Model, bView.Model);
            }
        }
    }
}
