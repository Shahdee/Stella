using UnityEngine;

namespace Level
{
    public class LevelDataProvider : ILevelDataProvider
    {
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