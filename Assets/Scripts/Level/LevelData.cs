using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Level
{
    [Serializable] [JsonObject(MemberSerialization.Fields)]
    public class LevelData
    {
        public List<Vector3Int> BlockPositions => _blockPositions;
        public int LevelId => _levelId;

        [SerializeField] private List<Vector3Int> _blockPositions;
        [SerializeField] private int _levelId;
        
        // next 
        // current level data 
        // level characters 
        // initial character position
        // character type 
        // exit position 
        
        public LevelData ShallowCopy()
        {
            return (LevelData) MemberwiseClone();
        }
    }
}