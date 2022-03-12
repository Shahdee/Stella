
using Block;
using UnityEngine;

public class BlockViewFactory : IBlockViewFactory
{
    private readonly BlockView _prefab;
    
    private int _blockId;
    
    public BlockViewFactory(BlockView prefab)
    {
        _prefab = prefab;
        _blockId = 0;
    }

    public BlockView CreateBlock(IBlockModel blockModel)
    {
        var block = Object.Instantiate(_prefab);

        block.name = block.name + _blockId;
        _blockId++;

        block.Initialize(blockModel);
        
        return block;
    }
}