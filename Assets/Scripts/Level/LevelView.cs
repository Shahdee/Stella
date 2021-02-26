using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelView : MonoBehaviour
{
    public Tilemap Level => _level;
    public PlatformEffector2D PlatformEffector => _platformEffector;
     
    [SerializeField] private Tilemap _level;
    [SerializeField] private TileBase _tileBlock;
    [SerializeField] private PlatformEffector2D _platformEffector;

    private const float InvertionTime = 0.5f;

    private bool _inverted;

    public Vector3Int GetTilePosition(Vector3 worldPosition)
    {
        return _level.WorldToCell(worldPosition);
    }

    public Vector3 GetCellCenterWorld(Vector3Int cellPosition)
    {
        return _level.GetCellCenterWorld(cellPosition);
    }

    public void InvertWorld(bool invert)
    {
        _inverted = invert;
        _platformEffector.rotationalOffset = _inverted ? 180 : 0;

        if (_inverted)
            StartCoroutine(CheckWorld());
    }

    public void SetTile(Vector3Int position)
    {
        var worldPosition = new Vector3Int(position.x, position.y, 0);
        _level.SetTile(worldPosition, _tileBlock);
    }

    public TileBase GetTile(Vector3Int position)
    {
        return _level.GetTile(position);
    }

    public void RemoveTile(Vector3Int position)
    {
        var worldPosition = new Vector3Int(position.x, position.y, 0);
        _level.SetTile(worldPosition, null);
    }

    private IEnumerator CheckWorld()
    {
        yield return new WaitForSeconds(InvertionTime);
        
        InvertWorld(false);
    }
}
