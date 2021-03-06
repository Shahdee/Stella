﻿using System;
using UnityEngine;

public class BlockView : MonoBehaviour
{
    public IBlockModel BlockModel => _blockModel;
    
    [SerializeField] private SpriteRenderer _blockSprite;

    private static Color DefaltColor = Color.white;
    
    private IBlockModel _blockModel;

    public void Initialize(IBlockModel model)
    {
        _blockModel = model;
        _blockModel.OnDestroy += BlockDestroy;
        _blockSprite.color = DefaltColor;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    } 
    
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    
    public void BlockDestroy(IBlockModel model)
    {
        _blockModel.OnDestroy -= BlockDestroy;
        Destroy(gameObject);
    }
}
