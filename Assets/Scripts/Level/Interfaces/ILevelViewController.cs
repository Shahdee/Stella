using System;
using UnityEngine;
using System.Collections.Generic;

public interface ILevelViewController
{
    event Action OnInitialize;
    
    Vector3 TransformPosition(Vector3Int cell);

    Vector3Int WorldToCell(Vector3 position);

    IBlockModel GetBlock(Vector3Int position);

    void AddInitialBlocks(List<Vector3Int> blockPositions);

    void InvertLevel(bool invert);
}