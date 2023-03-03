using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private float MovingSpeed = 2; // unit/sec
    [SerializeField] private float JumpSpeed = 2; // unit/sec
    [SerializeField] private bool JumpThroughForce = true;
    [SerializeField] private float GravityFallMultiplier = 2.5f;
    
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    public Vector2 Velocity => _rigid.velocity;

    private Vector2 _velocity;
    private bool _moving;
    
    public void SetPosition(Vector3 position)
    {
        _rigid.position = position;
    }

    public void Jump()
    {
        _rigid.velocity = Vector2.up * JumpSpeed;
    }

    public void ResolveJumpFalling(float fixedDeltaTime)
    {
        if (_rigid.velocity.y < 0)
        {
            _rigid.velocity += Vector2.up * (Physics2D.gravity.y * (GravityFallMultiplier - 1) * Time.fixedDeltaTime);
        }
    }

    public void Move(float direction)
    {
        var force = Mathf.Sign(direction) * MovingSpeed;
        _rigid.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        ClampVelocity();
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent, false);
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigid.velocity = velocity;
    }
    
    private void ClampVelocity()
    {
        // Debug.Log("clamp v " + _rigid.velocity);
        _velocity = _rigid.velocity;
        _velocity.x = Mathf.Clamp(_velocity.x, -MovingSpeed,MovingSpeed);
        // Debug.Log("clamp v " + _velocity.x);
        SetVelocity(_velocity);
    }
    
}

