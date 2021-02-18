using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelView : MonoBehaviour
{
    public Tilemap Level => _level;
     
    [SerializeField] private Tilemap _level;
    [SerializeField] private TileBase _tileBlock;


    public Vector3Int GetTilePosition(Vector3 worldPosition)
    {
        return _level.WorldToCell(worldPosition);
    }

    public void SetTile(Vector3Int position)
    {
        var worldPosition = new Vector3Int(position.x, position.y, 0);
        _level.SetTile(worldPosition, _tileBlock);
    }

    public void RemoveTile(Vector3Int position)
    {
        var worldPosition = new Vector3Int(position.x, position.y, 0);
        _level.SetTile(worldPosition, null);
    }
}
