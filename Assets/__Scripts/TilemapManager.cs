using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    static public Tile[] DELVER_TILES;

    [Header("Inscribed")]
    public Tilemap visualMap;

    private TileBase[] visualTileBaseArray;

    void Awake()
    {
        LoadTiles();
    }

    void Start()
    {
        ShowTiles();
    }

    /// <summary>
    /// Load all the Tiles from the Resources/Tiles_Visual folder into an array.
    /// </summary>
    void LoadTiles()
    {
        int num;

        // Load all of the Sprites from DelverTiles
        Tile[] tempTiles = Resources.LoadAll<Tile>("Tiles_Visual");

        // The order of the Tiles is not guaranteed, so arrange them by number
        DELVER_TILES = new Tile[tempTiles.Length];
        for (int i = 0; i < tempTiles.Length; i++)
        {
            string[] bits = tempTiles[i].name.Split('_');
            if (int.TryParse(bits[1], out num))
            {
                DELVER_TILES[num] = tempTiles[i];
            }
            else
            {
                Debug.LogError($"Failed to parse num of: {tempTiles[i].name}");
            }
        }
    }

    /// <summary>
    /// Uses GetMapTiles() to generate an array of TIleBases with the right tile
    ///  in each position on the map. Then set them as a block on visualMap.
    /// </summary>
    void ShowTiles()
    {
        visualTileBaseArray = GetMapTiles();
        visualMap.SetTilesBlock(MapInfo.GET_MAP_BOUNDS(), visualTileBaseArray);
    }

    /// <summary>
    /// Use MapInfo.MAP to create a TileBase[] array holding the tiles to fill
    ///  the visualMap Tilemap.
    /// </summary>
    /// <returns>TileBases for visualMap</returns>
    public TileBase[] GetMapTiles()
    {
        int tileNum;
        Tile tile;
        TileBase[] mapTiles = new TileBase[MapInfo.W * MapInfo.H];
        for (int y = 0; y < MapInfo.H; y++)
        {
            for (int x = 0; x < MapInfo.W; x++)
            {
                tileNum = MapInfo.MAP[x, y];
                tile = DELVER_TILES[tileNum];
                mapTiles[y * MapInfo.W + x] = tile;
            }
        }
        return mapTiles;
    }
}
