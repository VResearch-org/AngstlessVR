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
    [SerializeField] GameObject greeenhouseFront;
    [SerializeField] GameObject greeenhouseMiddle;
    [SerializeField] GameObject greeenhouseBack;

    public static int scale = 8;
    public static int difficulty = 3;
    public static int maxWidth = 9;
    private int levels = 0;
    private int currentWidth;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(window, new Vector3(-1 * scale, 0, 0), Quaternion.Euler(0, 90, 0));
        PopulateLevel(transform.position, maxWidth);
    }

    private void PopulateLevel(Vector3 initialTransform, int maxRange)
    {
        if (levels > 0)
            Instantiate(bridge, initialTransform, Quaternion.identity);

        Instantiate(wall, initialTransform + new Vector3(1 * scale, 0, 0), Quaternion.Euler(0, -90, 0));

        int minRange = difficulty - levels + 2 > 2 ? difficulty - levels + 2 : 2;

        int left = Random.Range(minRange, maxRange);
        int right = Random.Range(minRange, maxRange);
        currentWidth = left + right;

        levels++;

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

        int leftBack = Random.Range(3, 5);
        int rightBack = Random.Range(3, 5);
        if (rightBack == leftBack)
            leftBack++;

        for (int i = 1; i < leftBack; i++)
            Instantiate(utilityCorridor, initialTransform + new Vector3(i * scale, 0, -left * scale), Quaternion.Euler(0, 90, 0));

        for (int j = 1; j < rightBack; j++)
            Instantiate(utilityCorridor, initialTransform + new Vector3(j * scale, 0, right * scale), Quaternion.Euler(0, 90, 0));

        if (levels < difficulty)
        {
            PopulateLevel(initialTransform + new Vector3(leftBack * scale, 0, -left * scale), currentWidth/2);
            PopulateLevel(initialTransform + new Vector3(rightBack * scale, 0, right * scale), currentWidth/2);
        }
        else
        {
            Instantiate(greeenhouseFront, initialTransform + new Vector3(leftBack * scale, 0, -left * scale), Quaternion.identity);
            Instantiate(greeenhouseFront, initialTransform + new Vector3(rightBack * scale, 0, right * scale), Quaternion.identity);
            int greenhouseLeft = Random.Range(2, difficulty * 3);
            int greenhouseRight = Random.Range(2, difficulty * 3);
            for (int i = 1; i < greenhouseLeft; i++)
                Instantiate(greeenhouseMiddle, initialTransform + new Vector3((leftBack + i) * scale, 0, -left * scale), Quaternion.identity);

            for (int j = 1; j < greenhouseRight; j++)
                Instantiate(greeenhouseMiddle, initialTransform + new Vector3((rightBack + j) * scale, 0, right * scale), Quaternion.identity);

            Instantiate(greeenhouseBack, initialTransform + new Vector3((leftBack + greenhouseLeft) * scale, 0, -left * scale), Quaternion.identity);
            Instantiate(greeenhouseBack, initialTransform + new Vector3((rightBack + greenhouseRight) * scale, 0, right * scale), Quaternion.identity);
        }
    }
}
