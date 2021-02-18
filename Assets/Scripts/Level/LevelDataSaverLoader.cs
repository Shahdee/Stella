using System.IO;
using UnityEngine;

namespace Level
{
    public class LevelDataSaverLoader : ILevelDataSaverLoader
    {
        private const string LevelBaseName = "Level";
        private const string LevelFolder = "Levels/";
        
        private readonly ILevelDataSerializer _levelDataSerializer;

        public LevelDataSaverLoader(ILevelDataSerializer levelDataSerializer)
        {
            _levelDataSerializer = levelDataSerializer;
        }

        public void SaveLevel(LevelData levelData, int levelId)
        {
            var json = _levelDataSerializer.Serialize(levelData);
            
            // TODO 
        }
        
        public string LoadLevel(int levelId)
        {
            var directoryPath = Path.Combine(Application.persistentDataPath, LevelFolder);
            var levelName = LevelBaseName + levelId + ".json";
            var levelPath = Path.Combine(directoryPath, levelName);
            
            Debug.LogError(directoryPath);
            Debug.LogError(levelName);
            
            var json = File.ReadAllText (levelPath);
            
            Debug.Log("json " + json);

            return json;
        }
    }
}