using UnityEngine;
using UnityEngine.SceneManagement;

namespace Animation_Test
{
  public class SceneController : MonoBehaviour
  {
    private void OnEnable()
    {
      EventManager.OnResetBtnClicked += ResetScene;
    }

    private void OnDisable()
    {
      EventManager.OnResetBtnClicked -= ResetScene;
    }

    public void ResetScene()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }
}