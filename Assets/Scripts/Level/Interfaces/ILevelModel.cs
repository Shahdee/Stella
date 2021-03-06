using System.Collections.Generic;
using System;
using UnityEngine;

namespace Level
{
    public interface ILevelModel
    {
        event Action<IBlockModel> OnBlockPut;
        event Action<IBlockModel> OnBlockDestroy;
        event Action OnAllBlocksDestroy;
        
        HashSet<IBlockModel> LevelBlocks { get; }

        int CurrentLevel { get; }

        void Initialize(LevelData levelData);
        void PutBlock(IBlockModel blockModel);
        void DestroyAllBlocks();
        IBlockModel GetBlock(Vector3Int position);

        LevelData GetCurrentLevelData();

        void AdvanceLevel();
    }
}