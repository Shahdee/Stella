using System;
using Characters;

namespace Level
{
    public interface ILevelController
    {
        event Action OnLevelComplete;
        event Action<ICharacter> OnCharacterChange;

        void GenerateLevel();
        ICharacter GetCurrentCharacter();
        void ChangeCharacter();
    }
}