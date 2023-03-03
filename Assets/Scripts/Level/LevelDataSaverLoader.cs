using System.IO;
using System;
using UnityEngine;

namespace Level
{
    public class LevelDataSaverLoader : ILevelDataSaverLoader
    {
        private const string LevelBaseName = "Level";
        private const string LevelFolder = "Levels/";
        private const string JSON = ".json";
        private const string DefaultLevelJson = "{\"_blockPositions\":[],\"_levelId\":0}"; // TODO move it to another class ? 
        
        private readonly ILevelDataSerializer _levelDataSerializer;

        public LevelDataSaverLoader(ILevelDataSerializer levelDataSerializer)
        {
            _levelDataSerializer = levelDataSerializer;
        }

        public void SaveLevel(LevelData levelData)
        {
            var json = _levelDataSerializer.Serialize(levelData);
            
            var directoryPath = Path.Combine(Application.persistentDataPath, LevelFolder);
            var levelName = LevelBaseName + levelData.LevelId + JSON;
            
            var levelPath = Path.Combine(directoryPath, levelName);
            
            File.WriteAllText(levelPath, json);
        }
        
        public string LoadLevel(int levelId)
        {
            var directoryPath = Path.Combine(Application.persistentDataPath, LevelFolder);
            var levelName = LevelBaseName + levelId + JSON;
            var levelPath = Path.Combine(directoryPath, levelName);
            
            Debug.LogError(directoryPath);
            Debug.LogError(levelName);

            var json = DefaultLevelJson;

            try
            {
                json = File.ReadAllText(levelPath);
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(levelPath, DefaultLevelJson);
            }
            catch (Exception ex)
            {
                return null;
            }

            // Debug.Log("json " + json);

            return json;
        }
    }
}