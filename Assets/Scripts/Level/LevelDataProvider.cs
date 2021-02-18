using UnityEngine;


// TODO have initial json  

namespace Level
{
    public class LevelDataProvider : ILevelDataProvider
    {
        private const string LevelBaseName = "Level";
        private const string LevelFolder = "Levels/";
        
        private readonly ILevelDataSerializer _levelDataSerializer;
        private readonly ILevelDataSaverLoader _levelDataSaverLoader;

        public LevelDataProvider(ILevelDataSerializer levelDataSerializer,
                                ILevelDataSaverLoader levelDataSaverLoader)
        {
            _levelDataSerializer = levelDataSerializer;
            _levelDataSaverLoader = levelDataSaverLoader;
        }


        public LevelData LoadLevelData(int levelId)
        {
            var json = _levelDataSaverLoader.LoadLevel(levelId);
            if (string.IsNullOrEmpty(json))
                return null;

            return _levelDataSerializer.Deserialize(json);
        }
    }
}