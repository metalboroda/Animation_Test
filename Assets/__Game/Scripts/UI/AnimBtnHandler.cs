using TMPro;
using UnityEngine;

namespace Animation_Test
{
  public class AnimBtnHandler : MonoBehaviour
  {
    [field: SerializeField] public string BtnText { get; private set; }

    [field: Header("")]
    [field: SerializeField] public string AnimName { get; private set; }

    private TextMeshProUGUI _btnTextMeshPro;

    private void Awake()
    {
      _btnTextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
      SetupBtnTxt();
    }

    private void SetupBtnTxt()
    {
      _btnTextMeshPro.text = BtnText;
    }
  }
}