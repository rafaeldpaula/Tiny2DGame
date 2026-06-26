using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StartScreenScript : MonoBehaviour
{
    private const string FadeExitAnimationName = "FadeExit";

    private Animator _animator;
    private bool _transitionStarted;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_transitionStarted || !Input.anyKeyDown)
        {
            return;
        }

        _transitionStarted = true;
        _animator.Play(FadeExitAnimationName);
    }
}
