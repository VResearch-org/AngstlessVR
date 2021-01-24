using System;
using System.Collections.Generic;
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
        if(accessoryPositions.Length > 0)
        {
            foreach (Transform position in accessoryPositions)
            {
                levelBuilder.RegisterTile(position, accessories);
            }
        }
        
        foreach (Transform position in positions)
        {
            int c = UnityEngine.Random.Range(0, 2);
            if (c > 0)
            {
                if (accessories.Length > 0)
                {
                    if (accessoryPositions.Length == 0)
                        levelBuilder.PlaceTile(position, accessories);
                }
                else
                {
                    levelBuilder.RegisterTile(position, successors);
                }
            }
            else
            {
                levelBuilder.RegisterTile(position, successors);
            }
        }
    }
}
