using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Animation_Test
{
  public class UIManager : MonoBehaviour
  {
    [SerializeField] private List<Button> animButtons = new();

    [Header("")]
    [SerializeField] private Button iKEnableBtn;

    [Header("Game Screen")]
    [SerializeField] private Button resetBtn;

    private void Start()
    {
      SetupButtons();
    }

    private void SetupButtons()
    {
      foreach (var button in animButtons)
      {
        var btnAnimHandler = button.GetComponent<AnimBtnHandler>();

        button.onClick.AddListener(() =>
          {
            EventManager.RaiseAnimBtnCLicked(btnAnimHandler.AnimName,
            btnAnimHandler.LayerName);
          });
      }

      iKEnableBtn.onClick.AddListener(() => { EventManager.RaiseIKBtnClicked(); });

      resetBtn.onClick.AddListener(() => { EventManager.RaiseResetBtnClicked(); });
    }
  }
}