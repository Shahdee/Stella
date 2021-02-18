using System;
using Block;
using UnityEngine;

public interface IBlockModel
{
    event Action<IBlockModel> OnDestroy;
    Vector3Int Position { get; }
    void Destroy();
}