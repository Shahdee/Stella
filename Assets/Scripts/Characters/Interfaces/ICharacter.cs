using UnityEngine;

namespace Characters
{
    public interface ICharacter
    {
        ECharacterType CharacterType { get; }
        Vector3 Position { get; }
        void Teleport(Vector3 position);
        void Move(float direction);
        void Jump();
    }
}