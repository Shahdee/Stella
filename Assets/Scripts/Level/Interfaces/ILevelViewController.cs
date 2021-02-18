using UnityEngine;

public interface ILevelViewController
{
    Vector3 TransformPosition(Vector2Int position);

    Vector3Int WorldToCell(Vector3 position);
    
    BlockView GetBlock(IBlockModel block);
}