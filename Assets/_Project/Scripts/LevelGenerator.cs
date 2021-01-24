using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] Vector2 gridSize;
    [SerializeField] float distance;
    [SerializeField] GameObject center;
    [SerializeField] GameObject seed;

    private Vector2 offset;

    private void Awake()
    {
        offset = new Vector2(-distance * Mathf.FloorToInt(gridSize.x/2),distance * Mathf.FloorToInt(gridSize.y / 2));
        Debug.LogError(offset);
        for (int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                Instantiate(center, new Vector3(offset.x + (distance * i), 0, offset.y + (-distance * j)), Quaternion.identity);
            }
        }
    }
}
