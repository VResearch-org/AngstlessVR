using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject habitatCorridor;
    [SerializeField] GameObject utilityCorridor;
    [SerializeField] GameObject bridge;
    public static int scale = 8;
    public static int difficulty = 2;
    private int levels = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(window, new Vector3(-1 * scale, 0, 0), Quaternion.Euler(0, 90, 0));
        PopulateLevel(transform.position, difficulty*3);
    }

    private void PopulateLevel(Vector3 initialTransform, int maxRange)
    {
        if (levels > 0)
            Instantiate(bridge, initialTransform, Quaternion.identity);
        levels++;
        Instantiate(wall, initialTransform + new Vector3(1 * scale, 0, 0), Quaternion.Euler(0, -90, 0));

        int left = Random.Range(2, maxRange);
        int right = Random.Range(2, maxRange);

        for (int i = 1; i < left; i++)
            Instantiate(habitatCorridor, initialTransform + new Vector3(0, 0, -i * scale), Quaternion.identity);

        for (int j = 1; j < right; j++)
            Instantiate(habitatCorridor, initialTransform + new Vector3(0, 0, j * scale), Quaternion.identity);

        Instantiate(bridge, initialTransform + new Vector3(0, 0, -left * scale), Quaternion.identity);
        Instantiate(window, initialTransform + new Vector3(-1 * scale, 0, -left * scale), Quaternion.Euler(0, 90, 0));
        Instantiate(wall, initialTransform + new Vector3(0, 0, (-left - 1) * scale), Quaternion.identity);
        Instantiate(bridge, initialTransform + new Vector3(0, 0, right * scale), Quaternion.identity);
        Instantiate(window, initialTransform + new Vector3(-1 * scale, 0, right * scale), Quaternion.Euler(0, 90, 0));
        Instantiate(wall, initialTransform + new Vector3(0, 0, (right + 1) * scale), Quaternion.Euler(0, 180, 0));

        int leftBack = Random.Range(3, maxRange);
        int rightBack = Random.Range(4, maxRange);

        for (int i = 1; i < leftBack; i++)
            Instantiate(utilityCorridor, initialTransform + new Vector3(i * scale, 0, -left * scale), Quaternion.Euler(0, 90, 0));

        for (int j = 1; j < rightBack; j++)
            Instantiate(utilityCorridor, initialTransform + new Vector3(j * scale, 0, right * scale), Quaternion.Euler(0, 90, 0));

        if (levels < difficulty)
        {
            PopulateLevel(initialTransform + new Vector3(leftBack * scale, 0, -left * scale), left);
            PopulateLevel(initialTransform + new Vector3(rightBack * scale, 0, right * scale), right);
        }
        else
        {
            //instantiate greenhouse
        }
    }
}
