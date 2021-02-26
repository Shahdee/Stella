using System.Collections.Generic;

namespace Block
{
    public class BlockViewStorage : IBlockViewStorage
    {
        private readonly Dictionary<IBlockModel, BlockView> _blocks;
        
        public BlockViewStorage()
        {
            _blocks = new Dictionary<IBlockModel, BlockView>();
        }

        public void AddBlock(IBlockModel blockModel, BlockView blockView)
        {
            _blocks[blockModel] = blockView;
        }

        public BlockView GetBlock(IBlockModel blockModel)
        {
            if (_blocks.ContainsKey(blockModel))
                return _blocks[blockModel];

            return null;
        }

        public void RemoveBlock(IBlockModel blockModel)
        {
            if (_blocks.ContainsKey(blockModel))
                _blocks.Remove(blockModel);
        }
    }
}