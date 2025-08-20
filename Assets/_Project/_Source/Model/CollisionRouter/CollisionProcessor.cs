using System;
using System.Linq;
using System.Collections.Generic;

public class CollisionProcessor : IUpdatable, IDisposable
{
    private readonly List<CollisionInformation> _collisions;

    private IEnumerable<IRecord> _records;

    public CollisionProcessor(IEnumerable<IRecord> records)
    {
        _records = records;
        _collisions = new List<CollisionInformation>();
    }

    public void Update()
    {
        foreach (CollisionInformation information in _collisions)
        {
            (object, object) collision = information.Collision;
            IEnumerable<IRecord> records = _records.Where(record => record.IsTarget(collision));

            foreach (IRecord record in records)
                record.Do(collision);
        }

        _collisions.Clear();
    }

    public void Add(CollisionInformation information)
    {
        if (_collisions.Contains(information) == false)
            _collisions.Add(information);
    }

    public void Dispose()
    {
        _records = null;
    }
}