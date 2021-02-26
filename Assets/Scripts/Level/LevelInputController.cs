using Helpers;
using UnityEngine;


// TODO LevelInputController should be disabled when we are in menu 
// TODO jump down 

namespace Level
{
    public class LevelInputController : ILevelInputController, IUpdatable
    {
        private const string Jump = "Jump";
        
        private readonly ILevelController _levelController;
        private readonly ILevelViewController _levelViewController;
        private readonly IInputController _inputController;

        public LevelInputController(ILevelController levelController, 
                                    ILevelViewController levelViewController,
                                    IInputController inputController)
        {
            _levelController = levelController;
            _levelViewController = levelViewController;
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
             // TODO rewrite from axis 
             
             if (UnityEngine.Input.GetButtonDown("Vertical"))
             {
                 // Debug.Log("down ");
                 
                 var character = _levelController.GetCurrentCharacter();
                 if (character == null)
                     return;
                 
                 var cell = _levelViewController.WorldToCell(character.Position);
                    
                 var lowerNeighbour = cell;
                 lowerNeighbour.y -= 2;

                 var lowerBlock = _levelViewController.GetBlock(lowerNeighbour);
                 if (lowerBlock == null)
                     _levelViewController.InvertLevel(true);
             }
             
            if (UnityEngine.Input.GetButtonDown(Jump))
            {
                // Debug.Log("jump ");
                var character = _levelController.GetCurrentCharacter();
                if (character == null)
                    return;
                
                character.Jump();
            }
        } 
    }
}