using Helpers;
using UnityEngine;

namespace Characters
{
    public class CharacterController : ICharacter, IUpdatable
    {
        public ECharacterType CharacterType => _characterType;

        private CharacterView _view;
        private ECharacterType _characterType;
        private float _velocityDecrease;
        private bool _stopping;

        public CharacterController(ECharacterType characterType,
                                    CharacterView view)
        {
            _view = view;
            _characterType = characterType;
        }

        public void CustomUpdate(float deltaTime)
        {
            var viewportPoint = Camera.main.WorldToViewportPoint(_view.transform.position);
            
            if (viewportPoint.y < 0)
            {
                var nextViewportPoint = viewportPoint;
                nextViewportPoint.y = 1;
                var worldPoint =  Camera.main.ViewportToWorldPoint(nextViewportPoint);
                Teleport(worldPoint);
            }
        }

        public void Jump() => _view.Jump();

        public void Move(float direction) => _view.Move(direction);

        public void Teleport(Vector3 position)
        {
            _view.SetPosition(position);
            _view.SetVelocity(Vector2.zero);
        }
    }
}