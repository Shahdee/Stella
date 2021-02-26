using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Level
{
    public class LevelModel : ILevelModel
    {
        public event Action<IBlockModel> OnBlockPut;
        public event Action<IBlockModel> OnBlockDestroy;
        public event Action OnAllBlocksDestroy;

        public HashSet<IBlockModel> LevelBlocks => _levelBlocks;
        
        public int CurrentLevel => _currentLevel;

        private HashSet<IBlockModel> _levelBlocks;
        private LevelData _levelData;
        private LevelData _currentLevelData;
        private int _currentLevel;

        public LevelModel()
        {

        }

        public void Initialize(LevelData levelData)
        {
            _levelData = levelData;
            _currentLevelData = _levelData.ShallowCopy();
            
            _levelBlocks = new HashSet<IBlockModel>();
            _currentLevel = _levelData.LevelId;
        }

        public LevelData GetCurrentLevelData()
        {
            _currentLevelData.BlockPositions.Clear();

            foreach (var block in _levelBlocks)
                _currentLevelData.BlockPositions.Add(block.Position);

            return _currentLevelData;
        }

        public void AdvanceLevel()
        {
            _currentLevel++;
        }

        public void PutBlock(IBlockModel block)
        {
            // Debug.Log("Put at " + block.Position);
            
            _levelBlocks.Add(block);

            block.OnDestroy += BlockDestroy;

            OnBlockPut?.Invoke(block);
        }

        public void DestroyAllBlocks()
        {
            if (_levelData == null)
                return;

            foreach (var block in _levelBlocks)
                block.Destroy();

            _levelBlocks.Clear();
        }

        public IBlockModel GetBlock(Vector3Int position) => _levelBlocks.FirstOrDefault(b => b.Position.x == position.x && b.Position.y == position.y);

        private void BlockDestroy(IBlockModel blockModel)
        {
            blockModel.OnDestroy -= BlockDestroy;

            _levelBlocks.Remove(blockModel);
            OnBlockDestroy?.Invoke(blockModel);

            if (!_levelBlocks.Any())
                OnAllBlocksDestroy?.Invoke();
        }
    }
}