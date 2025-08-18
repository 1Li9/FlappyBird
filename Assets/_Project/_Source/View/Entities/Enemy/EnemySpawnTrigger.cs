using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemySpawnTrigger : MonoBehaviour, ISpawnEventPublisher, IPositional
{
    private Vector3 _startPosition;

    public Vector3 Position => transform.position;

    public event Action Spawning;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BirdTracker _))
        {
            Spawning?.Invoke();
            transform.position = _startPosition;
        }
    }

    public void SetPosition(Vector3 position)
    {
       transform.position = position;
    }
}