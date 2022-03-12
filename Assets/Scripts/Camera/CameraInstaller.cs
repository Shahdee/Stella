using Zenject;
using Cinemachine;

namespace Camera
{
    public class CameraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CinemachineVirtualCamera>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesTo<CameraController>().AsSingle().NonLazy(); 

        }
    }
}