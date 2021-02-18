using System.Collections;
using System.Collections.Generic;
using Block;
using UnityEngine;

public interface IBlockModelFactory
{
    IBlockModel CreateBlock(Vector3Int position);
}
