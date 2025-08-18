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
}