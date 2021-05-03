using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatTunnel : MonoBehaviour
{
    [SerializeField] GameObject habitat;

    private void Start()
    {
        Instantiate(habitat, transform.position + new Vector3(1 * GenerateLevel.scale, 0, 0), Quaternion.Euler(0,180,0));
        Instantiate(habitat, transform.position + new Vector3(-1 * GenerateLevel.scale, 0, 0), Quaternion.identity);
    }
}
