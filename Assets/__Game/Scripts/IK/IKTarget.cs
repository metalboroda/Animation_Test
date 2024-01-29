using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Animation_Test
{
  public class IKTarget : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
  {
    private bool _isDragging;

    private CapsuleCollider _capsuleCollider;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
      _capsuleCollider = GetComponent<CapsuleCollider>();
      _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
      EventManager.OnIKBtnClicked += EnableIKTarget;
    }

    private void Start()
    {
      _capsuleCollider.enabled = false;
    }

    private void OnDisable()
    {
      EventManager.OnIKBtnClicked -= EnableIKTarget;
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
  }
}