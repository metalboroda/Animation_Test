using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Animation_Test
{
  public class UIManager : MonoBehaviour
  {
    public event Action<string, string> AnimBtnCLicked;

    [Header("")]
    [SerializeField] private List<Button> buttons = new();

    private void Start()
    {
      SetupButtons();
    }

    private void SetupButtons()
    {
      foreach (var button in buttons)
      {
        var btnAnimHandler = button.GetComponent<AnimBtnHandler>();

        button.onClick.AddListener(
          () => AnimBtnCLicked?.Invoke(btnAnimHandler.AnimName, btnAnimHandler.LayerName));
      }
    }
  }
}