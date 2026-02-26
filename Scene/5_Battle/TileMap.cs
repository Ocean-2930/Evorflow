using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TileMap : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private int width = 5;

    private TokenTile[][] _tiles;
    public TokenTile[][] tiles { get { return _tiles; } }

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

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject spawnedTile = Instantiate(tile, transform);
                RectTransform spawnedRect = spawnedTile.GetComponent<RectTransform>();
                spawnedRect.anchoredPosition = new Vector2(x * tileWidth, y * tileHeight);

                TokenTile tokenTile = spawnedTile.GetComponent<TokenTile>();
                if (tokenTile == null)
                {
                    tokenTile = spawnedTile.AddComponent<TokenTile>();
                }

                _tiles[x][y] = tokenTile;
            }
        }
    }
}
