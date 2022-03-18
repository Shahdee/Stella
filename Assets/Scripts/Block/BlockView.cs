using UnityEngine;

public class BlockView : MonoBehaviour
{
    public IBlockModel BlockModel => _blockModel;
    
    [SerializeField] private SpriteRenderer _blockSprite;

    [SerializeField] private BoxCollider2D _blockCollider;

    private static Color DefaltColor = Color.white;
    // private static Color Tran = Color.white;
    
    private IBlockModel _blockModel;

    public void Initialize(IBlockModel model)
    {
        _blockModel = model;
        _blockModel.OnDestroy += BlockDestroy;
        // _blockSprite.color = DefaltColor;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    } 
    
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    
    void OnCollisionExit2D(Collision2D col)
    {
        // Debug.Log("Exit " + this.name);
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log("Enter " + this.name);
    }
    
    public void BlockDestroy(IBlockModel model)
    {
        _blockModel.OnDestroy -= BlockDestroy;
        Destroy(gameObject);
    }
}
