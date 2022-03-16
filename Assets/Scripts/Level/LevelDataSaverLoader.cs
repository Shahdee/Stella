using System.IO;
using Helpers;
using UnityEngine;

namespace Level
{
    public class LevelDataSaverLoader : ILevelDataSaverLoader
    {
        private const string LevelBaseName = "Level";
        private const string LevelFolder = "Levels/";
        private const string JSON = ".json";
        
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
            
            // Debug.LogError(directoryPath);
            // Debug.LogError(levelName);
            
            var json = File.ReadAllText (levelPath);
            
            // Debug.Log("json " + json);

            return json;
        }
    }
}