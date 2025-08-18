using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsView : MonoBehaviour, IStopable
{
    private List<Weapon> _weapons;

    private void Update()
    {
        if (_weapons == null || _weapons.Count == 0)
            return;

        foreach (Weapon weapon in _weapons)
            weapon.Tick(Time.deltaTime);
    }

    public void Add(Weapon weapon)
    {
        _weapons ??= new List<Weapon>();
        _weapons.Add(weapon);
    }

    public void Remove(Weapon weapon)
    {
        if(_weapons == null) 
            throw new InvalidOperationException(nameof(Remove));

        _weapons.Remove(weapon);
    }

    public void Stop()
    {
        _weapons.Clear();
    }
}