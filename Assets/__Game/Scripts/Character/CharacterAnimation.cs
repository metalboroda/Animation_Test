using System.Collections;
using UnityEngine;

namespace Animation_Test
{
  public class CharacterAnimation : MonoBehaviour
  {
    [Header("Animation Settings")]
    [SerializeField] private float crossDur = 0.25f;
    [SerializeField] private float returnDelaySubtr = 0.3f;
    [SerializeField] private string returnAnimName = "Idle";

    [Header("Layer Settings")]
    [SerializeField] private float layerDur = 0.15f;

    private Coroutine _animRoutine;

    private Animator _animator;

    private bool _canPlayAnimation = true;
    private bool _canPlayLayerAnimation = true;

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnEnable() => EventManager.OnAnimBtnCLicked += PlayAnimWithIdleEnd;

    private void OnDisable() => EventManager.OnAnimBtnCLicked -= PlayAnimWithIdleEnd;

    private void PlayAnimWithIdleEnd(string animName, string layerName)
    {
      if (string.IsNullOrEmpty(layerName))
        _animRoutine = StartCoroutine(DoPlayAnimWithIdleEnd(animName));
      else
        StartCoroutine(DoPlayerLayerAnimWithBaseEnd(layerName));
    }

    private IEnumerator DoPlayAnimWithIdleEnd(string animName)
    {
      if (CanPlayAnimation(animName) == false) yield break;

      CrossFadeAnimation(animName);

      yield return new WaitForSeconds(GetAnimationLength() - returnDelaySubtr);

      CrossFadeAnimation(returnAnimName);
    }

    private IEnumerator DoPlayerLayerAnimWithBaseEnd(string layerName)
    {
      if (_canPlayLayerAnimation == false) yield break;

      SetLayerWeightSmoothly(layerName, 1f, true);

      yield return new WaitForSeconds(GetAnimationLength(layerName) - returnDelaySubtr);

      SetLayerWeightSmoothly(layerName, 0f, false);

      yield return new WaitForSeconds(layerDur);

      _canPlayLayerAnimation = true;
    }

    private IEnumerator DoSmoothlySetLayerWeight(string layerName, float targetWeight,
      float duration, bool playAnimationAgain)
    {
      _canPlayLayerAnimation = false;

      int layerIndex = _animator.GetLayerIndex(layerName);
      float startTime = Time.time;
      float startWeight = _animator.GetLayerWeight(layerIndex);

      if (playAnimationAgain) _animator.Play(
        _animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash, layerIndex, 0f);

      while (Time.time < startTime + duration)
      {
        float t = Mathf.Clamp01((Time.time - startTime) / duration);

        _animator.SetLayerWeight(layerIndex, Mathf.Lerp(startWeight, targetWeight, t));

        yield return null;
      }

      _animator.SetLayerWeight(layerIndex, targetWeight);
    }

    private bool CanPlayAnimation(string animName) => _canPlayAnimation &&
      _animator.GetCurrentAnimatorStateInfo(0).IsName(animName) == false;

    private void CrossFadeAnimation(string animName)
    {
      if (_animRoutine != null) StopCoroutine(_animRoutine);

      _animator.CrossFadeInFixedTime(animName, crossDur);
      _canPlayAnimation = false;

      StartCoroutine(EnableAnimationAfterDelay(crossDur));
    }

    private IEnumerator EnableAnimationAfterDelay(float delay)
    {
      yield return new WaitForSeconds(delay);

      _canPlayAnimation = true;
    }

    private float GetAnimationLength() => _animator.GetCurrentAnimatorStateInfo(0).length;

    private float GetAnimationLength(string layerName)
    {
      int layerIndex = _animator.GetLayerIndex(layerName);

      return _animator.GetCurrentAnimatorStateInfo(layerIndex).length;
    }

    private void SetLayerWeightSmoothly(string layerName,
      float targetWeight, bool playAnimationAgain) => StartCoroutine(
        DoSmoothlySetLayerWeight(layerName, targetWeight, layerDur, playAnimationAgain));
  }
}