namespace Block
{
    public interface IBlockViewStorage
    {
        void AddBlock(IBlockModel blockModel, BlockView blockView);

        BlockView GetBlock(IBlockModel blockModel);
        void RemoveBlock(IBlockModel blockModel);
    }
}