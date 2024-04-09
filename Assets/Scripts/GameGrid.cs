using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameGrid : MonoBehaviour
{
    public Grid grid;
    public Canvas canvas;

    public CinemachineVirtualCamera virtualCamera;

    public Vector2Int gridSize;
    public Tile tileObject;
    public Tilemap tilemap;
    public Tile[,] tiles;

    // Start is called before the first frame update
    void Awake()
    {
        tiles = new Tile[gridSize.x, gridSize.y];

        tilemap.size = new Vector3Int(gridSize.x, gridSize.y, 0);
        Vector3Int position = new Vector3Int();

        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                position.Set(x, y, 0);
                //   var newTile = Instantiate(tileObject, tilemap.CellToLocal(position), Quaternion.identity);
                //   tiles[x, y] = newTile;
                TileBase tile = Resources.Load<TileBase>("Tiles/Water");
                tilemap.SetTile(position, tile);
            }
        }

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickTile();
        }
        // Make drag camera script
    }

    public void ClickTile()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tilePosition = grid.LocalToCell(position);

        // If the input space is within the bounds of the grid, it is a valid tap....
        if ((Mathf.RoundToInt(tilePosition.x) >= 0 && Mathf.RoundToInt(tilePosition.x) < gridSize.x) && (Mathf.RoundToInt(tilePosition.y) >= 0 && Mathf.RoundToInt(tilePosition.y) < gridSize.y))
        {
            // perform some click action
            var tile = Resources.Load<TileBase>("Tiles/Flame");
            tilemap.SetTile(new Vector3Int(Mathf.RoundToInt(tilePosition.x), Mathf.RoundToInt(tilePosition.y)), tile);
            return;
        }
    }
    public bool CheckTileClicked()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tilePosition = grid.LocalToCellInterpolated(position);

        if ((Mathf.RoundToInt(tilePosition.x) >= 0 && Mathf.RoundToInt(tilePosition.x) < gridSize.x) && (Mathf.RoundToInt(tilePosition.y) >= 0 && Mathf.RoundToInt(tilePosition.y) < gridSize.y))
        {
            // Check if tile is already clicked
            if (true/*tiles[Mathf.RoundToInt(tilePosition.x), Mathf.RoundToInt(tilePosition.y)].image.enabled*/)
            {
                // if is, stop play action...
                return true;
            }
        }
        // if isn't, allow play action...
        return false;
    }
}
