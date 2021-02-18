using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Level;

// Next 
    // level data is loaded from json 

namespace Level
{

    public class LevelController : ILevelController, IDisposable
    {
        public event Action OnLevelComplete;

        private const int DefaultLevel = 0;
        private const int DefaultCharacterXPos = 0;
        private const int DefaultCharacterYPos = 0;

        private readonly LevelView _levelView;
        private readonly IInputController _inputController;
        private readonly IAudioController _audioController;
        private readonly ILevelModel _levelModel;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly ICharacterFactory _characterFactory;
        private readonly ICharacterDatabase _characterDatabase;
        private readonly List<ICharacter> _levelCharacters;

        private IBlockModelFactory _blockFactory;
        private ILevelViewController _levelViewController;
        private LevelData _levelData;
        private bool _initialize = true;

        public LevelController(LevelView levelView,
            ILevelModel levelModel,
            ILevelDataProvider levelDataProvider,
            ILevelViewController levelViewController,
            ICharacterFactory characterFactory,
            ICharacterDatabase characterDatabase,
            IBlockModelFactory blockFactory,
            IInputController inputController,
            IAudioController audioController)
        {
            _levelView = levelView;
            _levelModel = levelModel;
            _levelDataProvider = levelDataProvider;
            _inputController = inputController;
            _audioController = audioController;
            _blockFactory = blockFactory;
            _levelViewController = levelViewController;
            _characterFactory = characterFactory;
            _characterDatabase = characterDatabase;

            _inputController.OnQuickTouch += QuickTouch;

            _levelCharacters = new List<ICharacter>();
        }

        public void GenerateLevel()
        {
            ClearLevel();

            if (_initialize)
            {
                _initialize = false;

                _levelData = _levelDataProvider.LoadLevelData(DefaultLevel);
                
                Debug.LogError("Level data " + _levelData.BlockPositions);
                Debug.LogError("Level id " + _levelData.LevelId);
                
                _levelModel.Initialize(_levelData);

                TryAddInitialBlocksToLevel(_levelData.BlockPositions);
            }
            else
                _levelModel.AdvanceLevel();

            // TODO get/create character using level data  

            var character = _characterDatabase.Get(ECharacterType.Stella);
            if (character == null)
                character = _characterFactory.CreateCharacter(ECharacterType.Stella);

            if (character != null)
            {
                _levelCharacters.Add(character);

                var position =
                    _levelViewController.TransformPosition(new Vector2Int(DefaultCharacterXPos, DefaultCharacterYPos));
                character.Teleport(position);
            }
            else
            {
                Debug.LogError("character wasn't created ");
            }
        }

        public ICharacter GetCurrentCharacter()
        {
            if (_levelCharacters.Any())
                return _levelCharacters[0];

            return null;
        }

        public void ChangeCharacter()
        {
            // TODO implement 
        }

        private void ClearLevel()
        {
            _levelModel.DestroyAllBlocks();

            // TODO remove chars 
        }

        private void QuickTouch(Vector3 position)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            var blockPosition = _levelViewController.WorldToCell(worldPosition);

            var block = _levelModel.GetBlock(blockPosition);
            if (block == null)
            {
                var blockModel = _blockFactory.CreateBlock(blockPosition);
                _levelModel.PutBlock(blockModel);
            }
            else
            {
                block.Destroy();
            }
        }

        private void TryAddInitialBlocksToLevel(List<Vector3Int> blockPositions)
        {
            if (blockPositions == null || !blockPositions.Any())
                return;
            
            foreach (var position in blockPositions)
            {
                var block = _levelModel.GetBlock(position);
                if (block == null)
                {
                    var blockModel = _blockFactory.CreateBlock(position);
                    _levelModel.PutBlock(blockModel);
                }
            }
        }

        public void Dispose()
        {
            _inputController.OnQuickTouch -= QuickTouch;
        }
    }
}