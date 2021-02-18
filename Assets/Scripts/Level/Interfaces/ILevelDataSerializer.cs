using System.Runtime.Serialization;

namespace Level
{
    public interface ILevelDataSerializer
    {
        string Serialize(LevelData levelData);

        LevelData Deserialize(string json);
    }
}