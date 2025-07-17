using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, IInputService
{
    private Dictionary<IInputInformation, List<Action>> _actions = new();

    private void Update()
    {
        foreach (IInputInformation inputInformation in _actions.Keys)
        {
            if (inputInformation.IsKeyPressed)
            {
                foreach (Action action in _actions[inputInformation])
                    action?.Invoke();
            }
        }
    }

    public void BindAction(Action actøon, IInputInformation information)
    {
        if (_actions.ContainsKey(information))
        {
            _actions[information].Add(actøon);
            return;
        }

        _actions.Add(information, new List<Action>()
        {
            actøon
        });
    }
}