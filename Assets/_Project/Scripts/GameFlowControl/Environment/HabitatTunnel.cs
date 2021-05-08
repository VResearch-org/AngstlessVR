using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VResearch.GameFlowControl.Environment
{
    public class HabitatTunnel : EnvironmentModule
    {
        [SerializeField] GameObject habitat;

        protected override void InitializeModule()
        {
            Instantiate(habitat, transform.position + new Vector3(1 * GenerateLevel.scale, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(habitat, transform.position + new Vector3(-1 * GenerateLevel.scale, 0, 0), Quaternion.identity);
        }
    }
}
