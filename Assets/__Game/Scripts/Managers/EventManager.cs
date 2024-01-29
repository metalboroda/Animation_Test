using UnityEngine.Events;

namespace Animation_Test
{
  public static class EventManager
  {
    #region UI
    public static event UnityAction<string, string> OnAnimBtnCLicked;
    public static void RaiseAnimBtnCLicked(
      string animName,
      string layerName) => OnAnimBtnCLicked?.Invoke(animName, layerName);

    public static event UnityAction OnIKBtnClicked;
    public static void RaiseIKBtnClicked() => OnIKBtnClicked?.Invoke();
    #endregion
  }
}