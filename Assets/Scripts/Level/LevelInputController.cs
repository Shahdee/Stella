using Helpers;
using UnityEngine;


// TODO LevelInputController should be disabled when we are in menu 

namespace Level
{
    public class LevelInputController : ILevelInputController, IUpdatable
    {
        private const string Jump = "Jump";
        
        private readonly ILevelController _levelController;
        private readonly IInputController _inputController;

        public LevelInputController(ILevelController levelController, 
                                    IInputController inputController)
        {
            _levelController = levelController;
            _inputController = inputController;

            _inputController.OnAxisHold += OnAxisHold;
        }

        private void OnAxisHold(Vector2 direction)
        {
            var character = _levelController.GetCurrentCharacter();
            
            if (character != null)
                character.Move(direction.x);
        }

        public void CustomUpdate(float deltaTime)
        {
            if (UnityEngine.Input.GetButtonDown(Jump))
            {
                var character = _levelController.GetCurrentCharacter();
            
                if (character != null)
                    character.Jump();
            }
        } 
    }
}