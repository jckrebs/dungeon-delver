using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class MapInfo : MonoBehaviour
{
    static public int W { get; private set; }
    static public int H { get; private set; }
    static public int [,] MAP { get; private set; }
    static public Vector3 OFFSET = new Vector3(0.5f, 0.5f, 0);

    [Header("Inscribed")]
    public TextAsset delverLevel;

    // Start is called before the first frame update
    void Start()
    {
        LoadMap();
    }

    /// <summary>
    /// Load map data from the delverLevel TextAsset (e.g., DelverLevel_Eagle)
    /// </summary>
    void LoadMap()
    {
        // Read in the map data as an array of lines
        string[] lines = delverLevel.text.Split('\n');
        H = lines.Length;
        string[] tileNums = lines[0].Trim().Split(' ');
        W = tileNums.Length;

        // Place the map data into a 2D Array for very fast access
        MAP = new int[W, H]; // Generate a 2D array of the right size
        for (int j = 0; j < H; j++)
        {
            tileNums = lines[j].Trim().Split(' ');
            for (int i = 0; i < W; i++)
            {
                if (tileNums[i] == "..")
                {
                    MAP[i, j] = 0;
                }
                else
                {
                    MAP[i, j] = int.Parse(tileNums[i], NumberStyles.HexNumber);
                }
            }
        }
        Debug.Log($"Map size: {W} wide by {H} high");
    }

    /// <summary>
    /// Used by TilemapManager to get the bounds of the map
    /// </summary>
    /// <returns></returns>
    public static BoundsInt GET_MAP_BOUNDS()
    {
        BoundsInt bounds = new BoundsInt(0, 0, 0, W, H, 1);
        return bounds;
    }
}
