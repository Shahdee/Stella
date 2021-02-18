using UnityEngine;
using System;
using Block;

public class BlockModel : IBlockModel
{
      public event Action<IBlockModel> OnDestroy;
      public Vector3Int Position => _position;
      private Vector3Int _position;
      
      public BlockModel(Vector3Int position)
      {
         _position = position;
      } 
      
      public void Destroy()
      {
         OnDestroy?.Invoke(this);
         OnDestroy = null;
      }
}
