using System.Collections.Generic;
using UnityEngine;

public enum MapDirection
{
    up = 0,
    right = 1,
    down = 2,
    left = 3
}

public class MapVector
{
    private List<int[]> _vlist = new List<int[]>();
    public List<int[]> vlist { get { return Twist(MapDirection.up); } }

    public void AddVector(int x, int y)
    {
        _vlist.Add(new int[2] { x, y });
    }

    public List<int[]> Twist(MapDirection direction)
    {
        List<int[]> rlist = new List<int[]>();

        for (int i = 0; i < _vlist.Count; i++)
        {
            int x = _vlist[i][0];
            int y = _vlist[i][1];

            switch (direction)
            {
                case MapDirection.right:
                    rlist.Add(new int[2] { y, -x });
                    break;
                case MapDirection.down:
                    rlist.Add(new int[2] { -x, -y });
                    break;
                case MapDirection.left:
                    rlist.Add(new int[2] { -y, x });
                    break;
                default:
                    rlist.Add(new int[2] { x, y });
                    break;
            }
        }

        return rlist;
    }
}

[RequireComponent(typeof(RectTransform))]
public class TileMap : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private int width = 5;

    private TokenTile[][] _tiles;
    public TokenTile[][] tiles { get { return _tiles; } }

    public List<TokenTile> GetTiles(int x, int y, MapVector vectors, MapDirection dir)
    {
        List<TokenTile> rlist = new List<TokenTile>();

        if (_tiles == null || vectors == null)
        {
            return rlist;
        }

        List<int[]> twisted = vectors.Twist(dir);

        for (int i = 0; i < twisted.Count; i++)
        {
            int tx = x + twisted[i][0];
            int ty = y + twisted[i][1];

            if (0 <= tx && tx < width && 0 <= ty && ty < width)
            {
                rlist.Add(_tiles[tx][ty]);
            }
        }

        return rlist;
    }

    private void Start()
    {
        if (tile == null)
        {
            Debug.LogError("TileMap: tile prefab is not assigned.");
            return;
        }

        RectTransform tileRect = tile.GetComponent<RectTransform>();
        if (tileRect == null)
        {
            Debug.LogError("TileMap: tile prefab must have RectTransform.");
            return;
        }

        _tiles = new TokenTile[width][];
        for (int x = 0; x < width; x++)
        {
            _tiles[x] = new TokenTile[width];
        }

        float tileWidth = tileRect.sizeDelta.x;
        float tileHeight = tileRect.sizeDelta.y;
        int centerOffset = width / 2;

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject spawnedTile = Instantiate(tile, transform);
                RectTransform spawnedRect = spawnedTile.GetComponent<RectTransform>();
                spawnedRect.anchoredPosition = new Vector2((-centerOffset * tileWidth) + (tileWidth * x), (-centerOffset * tileHeight) + (tileHeight * y));

                TokenTile tokenTile = spawnedTile.GetComponent<TokenTile>();
                if (tokenTile == null)
                {
                    tokenTile = spawnedTile.AddComponent<TokenTile>();
                }

                tokenTile.SetTilePosition(x, y);

                _tiles[x][y] = tokenTile;
            }
        }
    }
}
