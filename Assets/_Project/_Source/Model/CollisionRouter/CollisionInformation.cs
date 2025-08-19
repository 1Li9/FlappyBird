using System;

public readonly struct CollisionInformation
{
    public (object, object) Collision { get; }

    public CollisionInformation((object, object) collision)
    {
        Collision = collision;
    }

    public override string ToString()
    {
        return $"{Collision.Item1} -- {Collision.Item2}";
    }

    public override bool Equals(object obj)
    {
        if (obj is CollisionInformation information)
        {
            return information.Collision.Item1 == Collision.Item1 & information.Collision.Item2 == Collision.Item2 ||
                information.Collision.Item1 == Collision.Item2 & information.Collision.Item2 == Collision.Item1;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Collision);
    }
}