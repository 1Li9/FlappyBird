using System.Linq;
using System.Collections.Generic;

public class CollisionProcessor
{
    private readonly IEnumerable<IRecord> _records;

    private List<CollisionInformation> _collisions;

    public CollisionProcessor(IEnumerable<IRecord> records)
    {
        _records = records;
        _collisions = new List<CollisionInformation>();
    }

    public void Process()
    {
        foreach (CollisionInformation information in _collisions)
        {
            (object, object) collision = information.Collision;
            IEnumerable<IRecord> records = _records.Where(record => record.IsTarget(collision));

            foreach (IRecord record in records)
                record.Do(collision);
        }

        _collisions = new List<CollisionInformation>();
    }

    public void Add(CollisionInformation information)
    {
        if (_collisions.Contains(information) == false)
            _collisions.Add(information);
    }
}