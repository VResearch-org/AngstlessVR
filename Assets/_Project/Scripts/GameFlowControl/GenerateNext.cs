using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateNext : MonoBehaviour
{
    [SerializeField] GameObject[] successors;
    [SerializeField] GameObject[] accessories;
    [SerializeField] Transform[] positions;
    [SerializeField] Transform[] accessoryPositions;

    private LevelBuilder levelBuilder;

    private void Awake()
    {
        levelBuilder = LevelBuilder.Instance;
    }

    private void Start()
    {
        if(accessoryPositions.Count() > 0)
        {
            foreach (Transform position in accessoryPositions)
            {
                if (levelBuilder.RegisterTile(position))
                {
                    PlaceTile(position, accessories);
                }
            }
        }
        
        foreach (Transform position in positions)
        {
            int c = UnityEngine.Random.Range(0, 2);
            if (c > 0)
            {
                if (accessories.Count() > 0)
                {
                    if (accessoryPositions.Count() == 0)
                        PlaceTile(position, accessories);
                }
                else
                {
                    if (levelBuilder.RegisterTile(position))
                        PlaceTile(position, successors);
                }
            }
            else
            {
                if (levelBuilder.RegisterTile(position))
                    PlaceTile(position, successors);
            }
        }
    }

    private void PlaceTile(Transform position, GameObject[] pool)
    {
        int p = UnityEngine.Random.Range(0, pool.Count());
        GameObject go = Instantiate(pool[p]);
        go.transform.parent = position;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
    }
}
