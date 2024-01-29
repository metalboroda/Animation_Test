using UnityEngine;

public class Setup : MonoBehaviour
{
  private void Start()
  {
    Application.targetFrameRate = 120;
    QualitySettings.vSyncCount = 1;
  }
}