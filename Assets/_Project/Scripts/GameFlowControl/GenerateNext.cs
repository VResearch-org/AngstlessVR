using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateNext : MonoBehaviour
{
    [SerializeField] GameObject[] successors;
    [SerializeField] GameObject[] accessories;
    [SerializeField] bool accessoriesTakeTiles;
    [SerializeField] bool south, west, north, east;
    private List<int> positions;

    void Start()
    {
        GeneratePositions();
        Shuffle();
        if (accessories.Length > 0)
        {
            foreach (GameObject accessory in accessories)
            {
                int p = UnityEngine.Random.Range(0, 2);
                if (p == 1)
                {
                    Debug.Log("Placing " + accessory);
                    accessory.SetActive(true);
                    GetNextPosition(accessory, accessoriesTakeTiles);
                }
            }
        }
        //if(successors.Length > 0)
            //PopulateRemaining(positions.Count);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            PopulateNext();
        }
            
    }

    private void PopulateNext()
    {
        int p = UnityEngine.Random.Range(0, successors.Length);
        Debug.Log("Instantiating: " + successors[p]);
        GameObject neighbor = Instantiate(successors[p]);
        GetNextPosition(neighbor);
    }

    private void PopulateRemaining(int count)
    {
        for(int i = 0; i<count; i++)
        {
            int p = UnityEngine.Random.Range(0, successors.Length);
            Debug.Log("Instantiating: " + successors[p]);
            GameObject neighbor = Instantiate(successors[p]);
            GetNextPosition(neighbor);
        }
    }

    private void GeneratePositions()
    {
        positions = new List<int>();
        if (south)
            positions.Add(0);
        if (west)
            positions.Add(1);
        if (north)
            positions.Add(2);
        if (east)
            positions.Add(3);
    }

    private void GetNextPosition(GameObject go, bool takesTile = true)
    {
        if(positions.Count>0)
        {
            int r = positions[0];
            positions.RemoveAt(0);
            go.transform.rotation = Quaternion.Euler(0,r*90 + transform.rotation.y,0);
            if (takesTile)
            {
                string parentCoords = transform.parent.name;
                Transform target = FindTileParent(parentCoords, Wrap(r));
                if (target)
                {
                    Debug.Log("Parent found: " + target);
                    if(target.childCount == 0)
                    {
                        go.transform.position = target.position;
                        go.transform.parent = target;
                    }
                    else
                        Destroy(go);
                }
                else
                    Destroy(go);
            }
        }
    }

    private int Wrap(int r)
    {
        float angle = transform.rotation.y;
        if (angle < 0)
            angle += 360;

        r = (int)( (r + (angle / 90)) % 4);
        return r;
    }

    private Transform FindTileParent(string parentCoords, int r)
    {
        Vector2 currentTile, tileOffset, nextTile;

        string[] coords = parentCoords.Split(',');
        
        currentTile = new Vector2(int.Parse(coords[0]), int.Parse(coords[1]));

        if(r == 0) tileOffset = new Vector2(0, -1);
        else if(r == 1) tileOffset = new Vector2(-1, 0);
        else if(r == 2) tileOffset = new Vector2(0, 1);
        else tileOffset = new Vector2(1, 0);

        nextTile = currentTile + tileOffset;

        return transform.parent.parent.Find(nextTile.x + "," + nextTile.y);
    }

    private void Shuffle()
    {
        var rnd = new System.Random();
        positions = positions.OrderBy(item => rnd.Next()).ToList();
    }
}
