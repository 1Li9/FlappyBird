using System;
using UnityEngine;

public struct KeyboardInputInformation : IInputInformation
{
    private readonly KeyCode _keyCode;

    public KeyboardInputInformation(KeyCode keyCode)
    {
        _keyCode = keyCode;
    }

    public bool IsKeyPressed => Input.GetKeyDown(_keyCode);

    public override bool Equals(object obj)
    {
        return obj is KeyboardInputInformation information &&
               _keyCode == information._keyCode;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_keyCode);
    }
}