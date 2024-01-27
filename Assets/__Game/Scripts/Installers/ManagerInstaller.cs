using UnityEngine;
using Zenject;

namespace Animation_Test
{
  public class ManagerInstaller : MonoInstaller
  {
    [SerializeField] private UIManager uIManager;

    override public void InstallBindings()
    {
      Container.Bind<UIManager>().FromInstance(uIManager).AsSingle();
    }
  }
}