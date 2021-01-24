using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public float distance = 8;
    [SerializeField] GameObject seed;
    [SerializeField] GameObject gridRoot;
    public int maxTiles;
    public int currentTiles = 0;

    private static LevelBuilder _instance;
    public static LevelBuilder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelBuilder>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        seed.transform.parent = gridRoot.transform;
        currentTiles++;
    }

    public bool RegisterTile(Transform t, bool overrideCount = false)
    {
        string name = Mathf.RoundToInt(t.position.x / distance) + "," + Mathf.RoundToInt(t.position.z / distance);
        if (transform.Find(name))
        {
            return false;
        }
        else
        {
            if (currentTiles < maxTiles)
            {
                t.name = name;
                t.parent = this.transform;
                currentTiles++;
                return true;
            }
            else
                return false;
        }
    }
}
