using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderBroadcaster : MonoBehaviour
{
    private CollisionProcessor _processor;

    private object _model;

    public void Init(CollisionProcessor processor, object model)
    {
        _model = model;
        _processor = processor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ColliderBroadcaster broadcaster))
        {
            CollisionInformation information = new((_model, broadcaster._model));
            _processor.Process(information);
        }
    }
}