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

    public bool RegisterTile(Transform t, GameObject[] pool)
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
                PlaceTile(t, pool);
                return true;
            }
            else
                return false;
        }
    }

    public void PlaceTile(Transform position, GameObject[] pool)
    {
        int p = UnityEngine.Random.Range(0, pool.Length);
        GameObject go = Instantiate(pool[p]);
        go.transform.parent = position;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
    }
}
