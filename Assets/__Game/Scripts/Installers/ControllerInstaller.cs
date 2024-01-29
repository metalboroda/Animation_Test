using UnityEngine;
using Zenject;

namespace Animation_Test
{
  public class ControllerInstaller : MonoInstaller
  {
    [SerializeField] private SceneController _sceneController;

    public override void InstallBindings()
    {
      Container.Bind<SceneController>().FromInstance(_sceneController).AsSingle();
    }
  }
}