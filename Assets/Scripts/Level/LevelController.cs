using UnityEngine;
using System;
using System.Collections.Generic;
using Characters;
using Helpers;

// TODO Next 
    // next level is loaded from json   
    // moving between scenes 

namespace Level
{

    public class LevelController : ILevelController, IDisposable, IApplicationQuitListener, IApplicationPauseListener
    {
        public event Action OnLevelComplete;
        public event Action<ICharacter> OnCharacterChange;

        private const int DefaultLevel = 0;
        private const int DefaultCharacterXPos = 0;
        private const int DefaultCharacterYPos = 0;

        private readonly IAudioController _audioController;
        private readonly ILevelModel _levelModel;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly ICharacterFactory _characterFactory;
        private readonly ICharacterDatabase _characterDatabase;
        private readonly ILevelDataSaverLoader _levelDataSaverLoader;
        
        private readonly List<ICharacter> _levelCharacters;
        private ICharacter _currentCharacter;

        private ILevelViewController _levelViewController;
        private LevelData _levelData;
        private bool _initialize = true;
        private IApplicationPauseListener _applicationPauseListenerImplementation;

        public LevelController(ILevelModel levelModel,
                            ILevelDataProvider levelDataProvider,
                            ILevelViewController levelViewController,
                            ICharacterFactory characterFactory,
                            ICharacterDatabase characterDatabase,
                            ILevelDataSaverLoader levelDataSaverLoader,
                            
                            IAudioController audioController)
        {
            _levelModel = levelModel;
            _levelDataProvider = levelDataProvider;
            _audioController = audioController;
            _levelViewController = levelViewController;
            _characterFactory = characterFactory;
            _characterDatabase = characterDatabase;
            _levelDataSaverLoader = levelDataSaverLoader;

            _levelCharacters = new List<ICharacter>();
        }

        public void GenerateLevel()
        {
            ClearLevel();

            if (_initialize)
            {
                _initialize = false;
                
                // TODO init next level differently

                _levelData = _levelDataProvider.LoadLevelData(DefaultLevel);
                
                _levelModel.Initialize(_levelData);
                _levelViewController.AddInitialBlocks(_levelData.BlockPositions);
            }
            else
                _levelModel.AdvanceLevel();

            CreateCharacter();
        }

        public ICharacter GetCurrentCharacter()
        {
            return _currentCharacter;
        }

        public void ChangeCharacter()
        {
            // TODO implement 
            // set next character
            
            OnCharacterChange?.Invoke(_currentCharacter);
        }

        private void CreateCharacter()
        {
            // TODO
            // get/create character using level data
            // move to a separate class

            var character = _characterDatabase.Get(ECharacterType.Stella);
            if (character == null)
                character = _characterFactory.CreateCharacter(ECharacterType.Stella);

            _currentCharacter = character;

            if (character != null)
            {
                _levelCharacters.Add(character);

                var position =
                    _levelViewController.TransformPosition(new Vector3Int(DefaultCharacterXPos, DefaultCharacterYPos, 0));
                character.Teleport(position);
            }
            else
                Debug.LogError("character wasn't created ");
            
            OnCharacterChange?.Invoke(_currentCharacter);
        }

        private void ClearLevel()
        {
            _levelModel.DestroyAllBlocks();

            // TODO remove chars 
        }
        

        public void ApplicationQuit() => SaveLevel();
        
        public void ApplicationPause() => SaveLevel();

        private void SaveLevel()
        {
            var data = _levelModel.GetCurrentLevelData();
            _levelDataSaverLoader.SaveLevel(data);
        }

        public void Dispose()
        {
            
        }
    }
}