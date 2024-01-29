using RootMotion.FinalIK;
using UnityEngine;

namespace Animation_Test
{
  public class CharacterIKHandler : MonoBehaviour
  {
    [SerializeField] private LimbIK leftHandIK;

    [Header("")]
    [SerializeField] private Transform target;
    [SerializeField] private float stretchingLimit = 0.75f;

    private Vector3 _initTargetLocalPos;

    private void OnEnable()
    {
      EventManager.OnIKBtnClicked += EnableIK;
    }

    private void Start()
    {
      _initTargetLocalPos = target.localPosition;
      leftHandIK.solver.target = target;
      leftHandIK.enabled = false;
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
      leftHandIK.enabled = true;
    }
  }
}