﻿using System;
using UnityEngine;
using Object = System.Object;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private float MovingSpeed = 5; // unit/sec
    [SerializeField] private float JumpSpeed = 5; // unit/sec
    
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    public Vector2 Velocity => _rigid.velocity;

    private bool _moving;
    
    public void SetPosition(Vector3 position)
    {
        _rigid.position = position;
    }

    public void Jump()
    {
        // if (_rigid.velocity.y > 0.001f)
        //     return;

        _rigid.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);

        // ClampVelocity();
    }

    public void Move(float direction)
    {
        var force = 0f;
        
        if (direction > 0 || direction < 0)
            force = Mathf.Sign(direction) * MovingSpeed;
        else
            force = 0;
            
        _rigid.AddForce(Vector2.right * force);

        ClampVelocity();
    }

    private void ClampVelocity()
    {
        var velocity = _rigid.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -MovingSpeed,MovingSpeed);
        SetVelocity(velocity);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent, false);
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigid.velocity = velocity;
    }
}

