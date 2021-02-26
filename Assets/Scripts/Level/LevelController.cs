using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Helpers;

// Next 
    // move character creation to a separate class
    // next level is loaded from json   

namespace Level
{

    public class LevelController : ILevelController, IDisposable, IApplicationQuitListener, IApplicationPauseListener
    {
        public event Action OnLevelComplete;

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

            // TODO get/create character using level data  

            var character = _characterDatabase.Get(ECharacterType.Stella);
            if (character == null)
                character = _characterFactory.CreateCharacter(ECharacterType.Stella);

            if (character != null)
            {
                _levelCharacters.Add(character);

                var position =
                    _levelViewController.TransformPosition(new Vector3Int(DefaultCharacterXPos, DefaultCharacterYPos, 0));
                character.Teleport(position);
            }
            else
                Debug.LogError("character wasn't created ");
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