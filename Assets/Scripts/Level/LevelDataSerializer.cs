using Newtonsoft.Json;
using UnityEngine;


namespace Level
{
    public class LevelDataSerializer : ILevelDataSerializer
    {
        public LevelDataSerializer()
        {
            
        }

        public string Serialize(LevelData levelData)
        {
            string json = JsonConvert.SerializeObject(levelData);
            Debug.Log("level to json " + json);
            
            return json;
        }

        public LevelData Deserialize(string json)
        {
            var levelData =  JsonConvert.DeserializeObject<LevelData>(json);
            return levelData;
        }
    }
}