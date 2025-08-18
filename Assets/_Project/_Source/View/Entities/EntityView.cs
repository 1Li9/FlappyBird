using System;
using UnityEngine;

public class EntityView : MonoBehaviour
{
    private IEntity _entity;

    public IEntity Model => _entity;

    private void Update()
    {
        if (_entity == null)
            return;

        Synchronize();
    }

    private void OnDestroy()
    {
        if (_entity == null)
            return;

        _entity.OnDisable -= Disable;
        _entity.OnEnable -= Enable;
    }

    public void Init(IEntity entity)
    {
        if (_entity != null)
            throw new InvalidOperationException(nameof(Init));

        _entity = entity;

        Disable();
        Synchronize();
        Enable();

        _entity.OnDisable += Disable;
        _entity.OnEnable += Enable;
    }

    private void Enable()
    {
        gameObject.SetActive(true);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void Synchronize()
    {
        transform.position = _entity.Position;
        transform.localScale = _entity.Scale;
        transform.rotation = _entity.Rotation;
    }
}