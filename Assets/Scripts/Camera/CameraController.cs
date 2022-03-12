using System;
using Cinemachine;
using Characters;
using Level;

namespace Camera
{
    public class CameraController : IDisposable
    {
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly ILevelController _levelController;

        public CameraController(CinemachineVirtualCamera virtualCamera, 
                                ILevelController levelController)
        {
            _virtualCamera = virtualCamera;
            _levelController = levelController;

            _levelController.OnCharacterChange += ChangeCharacter;
        }

        private void ChangeCharacter(ICharacter character)
        {
            if (character == null)
                return;

            _virtualCamera.Follow = character.Body;
        }

        public void Dispose()
        {
            _levelController.OnCharacterChange -= ChangeCharacter;
        }
    }
}