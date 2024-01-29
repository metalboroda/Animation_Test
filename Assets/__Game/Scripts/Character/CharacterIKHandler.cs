using RootMotion.FinalIK;
using System.Collections;
using UnityEngine;

namespace Animation_Test
{
  public class CharacterIKHandler : MonoBehaviour
  {
    [SerializeField] private FullBodyBipedIK iK;

    [Header("")]
    [SerializeField] private Transform target;
    [SerializeField] private float weightDur = 0.15f;
    [SerializeField] private float stretchingLimit = 0.75f;

    private Vector3 _initTargetLocalPos;

    private void OnEnable()
    {
      EventManager.OnIKBtnClicked += EnableIK;
    }

    private void Start()
    {
      _initTargetLocalPos = target.localPosition;
      iK.solver.leftHandEffector.target = target;
      iK.enabled = false;
    }

    private void Update()
    {
      CheckTargetDistance();
    }

    private void OnDisable()
    {
      EventManager.OnIKBtnClicked -= EnableIK;
    }

    private void CheckTargetDistance()
    {
      float distance = Vector3.Distance(target.localPosition, _initTargetLocalPos);

      if (distance > stretchingLimit)
      {
      }
    }

    private void EnableIK()
    {
      iK.enabled = true;

      StartCoroutine(DoLerpIKPositionWeight(0f, 1f, weightDur, iK.solver.leftHandEffector));
    }

    private IEnumerator DoLerpIKPositionWeight(float startWeight, float endWeight,
      float duration, IKEffector limb)
    {
      float elapsedTime = 0f;

      while (elapsedTime < duration)
      {
        float t = elapsedTime / duration;

        limb.positionWeight = Mathf.Lerp(startWeight, endWeight, t);
        elapsedTime += Time.deltaTime;

        yield return null;
      }

      iK.solver.IKPositionWeight = endWeight;
    }
  }
}