using UnityEngine;
using System;
using Block;

public class BlockModel : IBlockModel
{
      public event Action<IBlockModel> OnDestroy;
      public event Action<IBlockModel> OnEat;

      public EBlockType BlockType => _blockType;
      public Color BlockColor => _color;
      public Vector3Int Position => _position;
      private Color _color;
      private Vector3Int _position;
      
      private EBlockType _blockType;
      
      public BlockModel(Vector3Int position)
      {
         _position = position;
      } 
      
      public BlockModel(EBlockType blockType,
                        Vector3Int position)
      { 
        _blockType = blockType;
        _position = position;
      }

      public void Destroy()
      {
         OnDestroy?.Invoke(this);
         OnDestroy = null;
      }
}
