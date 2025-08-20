using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : MonoBehaviour, IDisposable, IPausable
{
    public static class AnimatorData
    {
        public static int Jump = Animator.StringToHash(nameof(Jump));
    }

    private Animator _animator;

    private void Awake()
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

    public void Dispose()
    {
        _animator.enabled = false;
    }

    public void SetTigger(int id)
    {
        _animator.SetTrigger(id);
    }
}