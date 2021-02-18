using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{

    public class LevelViewController : ILevelViewController, IDisposable
    {
        private readonly ILevelModel _levelModel;
        private readonly LevelView _levelView;
        private readonly IBlockViewFactory _blockViewFactory;

        private const int BlockWidth = 1;
        private const int BlockHeight = 1;

        private Dictionary<IBlockModel, BlockView> _blocks;

        public LevelViewController(LevelView levelView,
            IBlockViewFactory blockViewFactory,
            ILevelModel levelModel)
        {
            _levelModel = levelModel;
            _levelView = levelView;
            _blockViewFactory = blockViewFactory;

            _blocks = new Dictionary<IBlockModel, BlockView>();

            _levelModel.OnBlockPut += BlockPut;
            _levelModel.OnBlockDestroy += BlockDestroy;
        }

        public Vector3Int WorldToCell(Vector3 position) => _levelView.GetTilePosition(position);

        public Vector3 TransformPosition(Vector2Int position)
        {
            return new Vector3()
            {
                x = position.x * BlockWidth + BlockWidth / 2,
                y = position.y * BlockHeight + BlockHeight / 2
            };
        }

        public BlockView GetBlock(IBlockModel block)
        {
            if (_blocks.ContainsKey(block))
                return _blocks[block];

            return null;
        }

        private void BlockPut(IBlockModel blockModel)
        {
            _levelView.SetTile(blockModel.Position);
        }

        private void BlockDestroy(IBlockModel blockModel)
        {
            _levelView.RemoveTile(blockModel.Position);
        }


        public void Dispose()
        {
            _levelModel.OnBlockPut -= BlockPut;
            _levelModel.OnBlockDestroy -= BlockDestroy;
        }
    }
}