using System.Collections.Generic;
using UnityEngine;

public class MapLocation
{
    public int x;
    public int z;

    public MapLocation(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}

public class Maze : MonoBehaviour
{
    protected List<MapLocation> directions = new List<MapLocation>() {
        new MapLocation(0, 1), new MapLocation(1, 0), new MapLocation(-1, 0), new MapLocation (0, -1)};

    [SerializeField] protected int width = 30; // x dimension
    [SerializeField] protected int depth = 30; // z dimension
    [SerializeField] protected int scale = 1;
    [SerializeField] protected GameObject bridge;
    [SerializeField] protected GameObject corridor;
    [SerializeField] protected GameObject habitatTunnel;
    [SerializeField] protected GameObject habitat;

    protected byte[,] map;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseMap();
        Generate();
        DrawMap();
    }

    private void InitialiseMap()
    {
        map = new byte[width, depth];

        for (int z = 0; z < depth; z++)
            for (int x = 0; x < width; x++)
            {
                map[x, z] = 1; //1 = wall, 0 = corridor
            }
    }

    public virtual void Generate()
    {
        for (int z = 0; z < depth; z++)
            for (int x = 0; x < width; x++)
            {
                if(Random.Range(0,2) == 0)
                    map[x, z] = 0; //1 = wall, 0 = corridor
            }
    }

    private void DrawMap()
    {
        for (int z = 0; z < depth; z++)
            for (int x = 0; x < width; x++)
            {
                if (map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x * scale, 0, z * scale);
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.position = pos;
                    wall.transform.localScale = new Vector3(scale, scale, scale);
                }
                else if (Search2d(x, z, new int[] { 5, 1, 5, 0, 0, 1, 5, 1, 5 }))
                {
                    Vector3 pos = new Vector3(x * scale, 0, z * scale);
                    GameObject go = Instantiate(habitat);
                    go.transform.position = pos;
                    go.transform.Rotate(Vector3.up, 180);
                }
                else if (Search2d(x, z, new int[] { 5, 1, 5, 1, 0, 0, 5, 1, 5 }))
                {
                    Vector3 pos = new Vector3(x * scale, 0, z * scale);
                    GameObject go = Instantiate(habitat);
                    go.transform.position = pos;
                }
                else if (Search2d(x, z, new int[] { 5, 0, 5, 1, 0, 1, 5, 0, 5 }))
                {
                    Vector3 pos = new Vector3(x * scale, 0, z * scale);
                    GameObject go = Instantiate(corridor);
                    go.transform.position = pos;
                }
                else if (Search2d(x, z, new int[] { 5, 1, 5, 0, 0, 0, 5, 1, 5 }))
                {
                    Vector3 pos = new Vector3(x * scale, 0, z * scale);
                    GameObject go = Instantiate(corridor);
                    go.transform.position = pos;
                    go.transform.Rotate(Vector3.up,90); 
                }
                else if (Search2d(x, z, new int[] { 5, 0, 5, 0, 0, 0, 5, 0, 5 }))
                {
                    Vector3 pos = new Vector3(x * scale, 0, z * scale);
                    GameObject go = Instantiate(bridge);
                    go.transform.position = pos;
                }
            }
    }

    public bool Search2d(int c, int r, int[] pattern)
    {
        int count = 0;
        int pos = 0;
        for(int z = 1; z > -2; z--)
        {
            for(int x = -1; x <2; x++)
            {
                if (pattern[pos] == map[c + x, r + z] || pattern[pos] == 5)
                    count++;

                pos++;
            }
        }
        return count == 9;
    }

    public int CounSquareNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x - 1, z] == 0) count++;
        if (map[x + 1, z] == 0) count++;
        if (map[x, z - 1] == 0) count++;
        if (map[x, z + 1] == 0) count++;
        return count;
    }

    public int CountDiagonalNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x - 1, z - 1] == 0) count++;
        if (map[x + 1, z + 1] == 0) count++;
        if (map[x - 1, z + 1] == 0) count++;
        if (map[x + 1, z - 1] == 0) count++;
        return count;
    }

    public int CountAllNeighbours(int x, int z)
    {
        return CounSquareNeighbours(x, z) + CountDiagonalNeighbours(x, z);
    }
}
