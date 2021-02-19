using System;
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
            // Debug.Log("level to json " + json);
            
            return json;
        }
        
        public LevelData Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<LevelData>(json);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return null;
        }
    }
}