using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] Vector2 gridSize;
    [SerializeField] float distance;
    [SerializeField] GameObject seed;
    [SerializeField] GameObject gridRoot;

    private Vector2 offset;

    private void Awake()
    {
        offset = new Vector2(-distance * Mathf.FloorToInt(gridSize.x/2),
            distance * Mathf.FloorToInt(gridSize.y / 2));

        for (int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                GameObject gridPoint = Instantiate(gridRoot, new Vector3(offset.x + (distance * x), 0, 
                    offset.y + (-distance * y)), Quaternion.identity);

                gridPoint.name = gridPoint.transform.position.x/distance + "," +
                    gridPoint.transform.position.z/distance;
                
                gridPoint.transform.parent = transform;
            }
        }
        seed.transform.parent = transform.Find("0,0");
        Destroy(gridRoot);
    }
}
