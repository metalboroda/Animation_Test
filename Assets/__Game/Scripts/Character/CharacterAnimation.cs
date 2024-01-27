using System.Collections;
using UnityEngine;
using Zenject;

namespace Animation_Test
{
  public class CharacterAnimation : MonoBehaviour
  {
    [SerializeField] private float crossDur = 0.25f;
    //[SerializeField] private float dampTime = 0.15f;
    [SerializeField] private float returnDelaySubtr = 0.3f;

    private Coroutine _animRoutine;

    private Animator _animator;

    [Inject] private readonly UIManager _uIManager;

    private bool _canPlayAnimation = true;

    private void Awake()
    {
      _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
      _uIManager.AnimBtnCLicked += PlayAnimWithIdleEnd;
    }

    private void OnDisable()
    {
      _uIManager.AnimBtnCLicked -= PlayAnimWithIdleEnd;
    }

    private void PlayAnimWithIdleEnd(string animName)
    {
      _animRoutine = StartCoroutine(DoPlayAnimWithIdleEnd(animName));
    }

    private IEnumerator DoPlayAnimWithIdleEnd(string animName)
    {
      if (_canPlayAnimation == false ||
        _animator.GetCurrentAnimatorStateInfo(0).IsName(animName)) yield break;

      if (_animRoutine != null)
      {
        StopCoroutine(_animRoutine);
      }

      _animator.CrossFadeInFixedTime(animName, crossDur);
      _canPlayAnimation = false;

      yield return new WaitForSeconds(crossDur);

      _canPlayAnimation = true;

      AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
      float animationLength = stateInfo.length;

      yield return new WaitForSeconds(animationLength - returnDelaySubtr);

      _animator.CrossFadeInFixedTime("Idle", crossDur);
    }
  }
}