using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : MonoBehaviour, IStopable, IPausable
{
    private const string Jump = nameof(Jump);

    private Animator _animator;

    public void Init()
    {
        _animator = GetComponent<Animator>();
    }

    public void Pause()
    {
        _animator.enabled = false;
    }

    public void Play()
    {
        _animator.enabled = true;
    }

    public void Stop()
    {
        _animator.enabled = false;
    }
    public void SetTigger(string name)
    {
        _animator.SetTrigger(name);
    }
}