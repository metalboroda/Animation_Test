using RootMotion.Dynamics;
using UnityEngine;

namespace Animation_Test
{
  public class CharacterRagdollHandler : MonoBehaviour
  {
    private PuppetMaster _puppetMaster;

    private CharacterIKHandler _characterIKHandler;

    private void Awake()
    {
      _puppetMaster = transform.root.GetComponentInChildren<PuppetMaster>();

      _characterIKHandler = GetComponent<CharacterIKHandler>();
    }

    private void OnEnable()
    {
      _characterIKHandler.LimitsPushed += EnableRagdoll;
    }

    private void OnDisable()
    {
      _characterIKHandler.LimitsPushed -= EnableRagdoll;
    }

    private void EnableRagdoll()
    {
      _puppetMaster.mappingWeight = 1f;
      _puppetMaster.pinWeight = 0f;
    }
  }
}