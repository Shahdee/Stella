using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Block;

namespace Level
{

    public class LevelViewController : ILevelViewController, IDisposable
    {
        private readonly ILevelModel _levelModel;
        private readonly LevelView _levelView;
        private readonly IBlockViewFactory _blockViewFactory;
        private readonly IBlockModelFactory _blockModelFactory;
        private readonly IBlockViewStorage _blockViewStorage;
        private readonly IInputController _inputController;

        private bool _initialized;

        public LevelViewController(LevelView levelView,
                                IBlockViewFactory blockViewFactory,
                                IBlockModelFactory blockModelFactory,
                                IBlockViewStorage blockViewStorage,
                                IInputController inputController,
                                ILevelModel levelModel)
        {
            _levelModel = levelModel;
            _levelView = levelView;
            _blockViewFactory = blockViewFactory;
            _blockModelFactory = blockModelFactory;
            _blockViewStorage = blockViewStorage;
            _inputController = inputController;

            _levelModel.OnBlockPut += BlockPut;
            _levelModel.OnBlockDestroy += BlockDestroy;

            _inputController.OnQuickTouch += QuickTouch;
        }

        public Vector3Int WorldToCell(Vector3 position) => _levelView.GetTilePosition(position);
        public IBlockModel GetBlock(Vector3Int position) => _levelModel.GetBlock(position);

        public Vector3 TransformPosition(Vector3Int position) => _levelView.GetCellCenterWorld(position);

        public void AddInitialBlocks(List<Vector3Int> blockPositions)
        {
            if (blockPositions == null || !blockPositions.Any())
                return;
            
            foreach (var position in blockPositions)
            {
                var block = _levelModel.GetBlock(position);
                if (block == null)
                {
                    var blockModel = _blockModelFactory.CreateBlock(position);
                    _levelModel.PutBlock(blockModel);
                }
            }

            _initialized = true;
            AddInitialColliders();
        }

        public void InvertLevel(bool invert) => _levelView.InvertWorld(invert);

        private void AddInitialColliders()
        {
            foreach (var pos in _levelView.Level.cellBounds.allPositionsWithin)
            {
                var block = _levelModel.GetBlock(pos);
                if (block == null)
                    continue;

                if (HasNeighbours(block))
                    AddCollider(block);
            }
        }

        private bool HasNeighbours(IBlockModel block)
        {
            var upperBlockPosition = block.Position;
            upperBlockPosition.y += 1;  
                
            var lowerBlockPosition = block.Position;
            lowerBlockPosition.y -= 1;

            return _levelModel.GetBlock(upperBlockPosition) != null || _levelModel.GetBlock(lowerBlockPosition) != null;
        }

        private void QuickTouch(Vector3 position)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            var cellPosition = WorldToCell(worldPosition);
            
            var block = _levelModel.GetBlock(cellPosition);
            if (block == null)
            {
                var blockModel = _blockModelFactory.CreateBlock(cellPosition);
                _levelModel.PutBlock(blockModel);
            }
            else
            {
                var upperBlock = GetNeighbour(block.Position, 1);
                var lowerBlock = GetNeighbour(block.Position, -1);
                
                block.Destroy();

                if (upperBlock != null && !HasNeighbours(upperBlock))
                    RemoveColliderBlock(upperBlock);
                
                if (lowerBlock != null && !HasNeighbours(lowerBlock))
                    RemoveColliderBlock(lowerBlock);
            }
        }

        private void BlockPut(IBlockModel block)
        {
            _levelView.SetTile(block.Position);

            if (!_initialized)
                return;

            var upperBlock = GetNeighbour(block.Position, 1);
            if ( upperBlock != null)
                AddCollider(upperBlock);
            
            var lowerBlock = GetNeighbour(block.Position, -1);
            if (lowerBlock != null)
                AddCollider(lowerBlock);
            
            if (upperBlock != null || lowerBlock != null)
                AddCollider(block);
        }

        private IBlockModel GetNeighbour(Vector3Int position, int yDirection)
        {
            var nextPosition = position;
            nextPosition.y += yDirection;
            
            return _levelModel.GetBlock(nextPosition);
        }
        
        private void AddCollider(IBlockModel blockModel)
        {
            var blockView = _blockViewStorage.GetBlock(blockModel);
            if (blockView != null)
            {
                Debug.LogError(blockModel.Position + " already has a collider view");
                return;
            }
            
            blockView = _blockViewFactory.CreateBlock(blockModel);
            blockView.SetParent(_levelView.transform);

            var position = TransformPosition(blockModel.Position);
            blockView.SetPosition(position);
            
            _blockViewStorage.AddBlock(blockModel, blockView);
        }

        private void RemoveColliderBlock(IBlockModel blockModel)
        {
            var blockView = _blockViewStorage.GetBlock(blockModel);
            if (blockView != null)
            {
                blockView.BlockDestroy(blockModel);       
                _blockViewStorage.RemoveBlock(blockModel);
            }
        }

        private void BlockDestroy(IBlockModel blockModel)
        {
            _levelView.RemoveTile(blockModel.Position);
        }


        public void Dispose()
        {
            _levelModel.OnBlockPut -= BlockPut;
            _levelModel.OnBlockDestroy -= BlockDestroy;
            _inputController.OnQuickTouch -= QuickTouch;
        }
    }
}