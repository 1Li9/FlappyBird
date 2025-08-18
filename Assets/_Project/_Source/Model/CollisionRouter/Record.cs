using System;

public class Record<T1, T2> : IRecord
{
    private readonly Action<T1, T2> _action;

    public Record(Action<T1, T2> action)
    {
        _action = action;
    }

    public void Do((object, object) collision)
    {
        object a = collision.Item1;
        object b = collision.Item2;

        if (a is T1 a1 && b is T2 b2)
            Do(a1, b2);
        else if (a is T2 b1 && b is T1 a2)
            Do(a2, b1);
        else
            throw new InvalidOperationException(nameof(Do));
    }

    private void Do(T1 a, T2 b)
    {
        _action?.Invoke(a, b);
    }

    private void Do(T2 b, T1 a)
    {
        _action?.Invoke(a, b);
    }

    public bool IsTarget((object, object) collision)
    {
        if (collision.Item1 is T1 && collision.Item2 is T2)
            return true;

        if (collision.Item1 is T2 && collision.Item2 is T1)
            return true;

        return false;
    }
}