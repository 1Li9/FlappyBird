using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProcessor
{
    private readonly IEnumerable<IRecord> _records;

    public CollisionProcessor(IEnumerable<IRecord> records)
    {
        _records = records;
    }

    public void Process(CollisionInformation information)
    {
        (object, object) collision = information.Collision;
        IEnumerable<IRecord> records = _records.Where(record => record.IsTarget(collision));

        foreach (IRecord record in records)
            record.Do(collision);
    }
}
