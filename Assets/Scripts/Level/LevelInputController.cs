using Helpers;
using UnityEngine;


// TODO LevelInputController should be disabled when we are in menu 
// TODO jump down rewrite from axis 

namespace Level
{
    public class LevelInputController : ILevelInputController, IUpdatable, IFixedUpdatable
    {
        private const string Jump = "Jump";
        
        private readonly ILevelController _levelController;
        private readonly ILevelViewController _levelViewController;
        private readonly IInputController _inputController;

        private bool _holding = false;
        private Vector2 _movingDirection;

        public LevelInputController(ILevelController levelController, 
                                    ILevelViewController levelViewController,
                                    IInputController inputController)
        {
            _levelController = levelController;
            _levelViewController = levelViewController;
            _inputController = inputController;

            _inputController.OnAxisHold += OnAxisHold;
            _inputController.OnAxisRelease += OnAxisRelease;
        }

        private void OnAxisHold(Vector2 direction)
        {
            _holding = true;
            _movingDirection = direction;
            // Debug.Log("hold " + direction.x);
        }

        private void OnAxisRelease(Vector2 direction)
        {
            _holding = false;
            // Debug.LogError("release " + direction.x);
        }

        public void CustomUpdate(float deltaTime)
        {
            if (UnityEngine.Input.GetButtonDown("Vertical"))
            {
                var character = _levelController.CurrentCharacter;
                 if (character == null)
                     return;
                 
                 var cell = _levelViewController.WorldToCell(character.Position);
                    
                 var downDownCell = cell;
                 downDownCell.y -= 2;

                 var downDownBlock = _levelViewController.GetBlock(downDownCell);
                 if (downDownBlock == null)
                     _levelViewController.InvertLevel(true);
            }    
             
            if (UnityEngine.Input.GetButtonDown(Jump))
            {
                var character = _levelController.CurrentCharacter;
                if (character == null)
                    return;
                
                character.Jump();
            }
        }

        public void CustomFixedUpdate(float fixedDeltaTime)
        {
            // // Debug.Log("f");
            var character = _levelController.CurrentCharacter;
            if (character != null && _holding)
            {
                Debug.LogError("fupd " + _movingDirection.x);
                character.Move(_movingDirection.x);
            }
        }
    }
}