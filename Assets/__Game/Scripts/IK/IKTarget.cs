using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Animation_Test
{
  public class IKTarget : MonoBehaviour, IPointerDownHandler, IDragHandler,
    IPointerUpHandler
  {
    private bool _isDragging;

    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;
    private MeshRenderer _meshRenderer;

    private CharacterIKHandler _characterIKHandler;

    private void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _capsuleCollider = GetComponent<CapsuleCollider>();
      _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
      EventManager.OnIKBtnClicked += EnableIKTarget;
    }

    private void OnDisable()
    {
      EventManager.OnIKBtnClicked -= EnableIKTarget;

      if (_characterIKHandler != null)
        _characterIKHandler.LimitsPushed -= MakePhysical;
    }

    public void Init(CharacterIKHandler characterIKHandler)
    {
      _characterIKHandler = characterIKHandler;
      _characterIKHandler.LimitsPushed += MakePhysical;
    }

    private void EnableIKTarget()
    {
      _capsuleCollider.enabled = true;

      EnableModel();
    }

    private void EnableModel()
    {
      var scale = _meshRenderer.transform.localScale;

      _meshRenderer.transform.localScale = Vector3.zero;
      _meshRenderer.enabled = true;
      _meshRenderer.transform.DOScale(scale, 0.15f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!_isDragging) return;

      Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
          -Camera.main.transform.position.z + transform.position.z);
      Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

      objPos.z = transform.position.z;
      transform.position = objPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      _isDragging = false;
    }

    private void MakePhysical()
    {
      transform.SetParent(null);
      _rigidbody.isKinematic = false;
      _rigidbody.velocity = Vector3.zero;
      _capsuleCollider.isTrigger = false;
      enabled = false;
    }
  }
}