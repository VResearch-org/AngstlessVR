using System;
using System.Collections.Generic;
using UnityEngine;
using VResearch.GameFlowControl.Environment;

namespace VResearch.GameFlowControl
{
    public class SpawnGameAssets : Singleton<SpawnGameAssets>
    {
        public enum ModuleType
        {
            bridge, tunnel, habitat, greenhouse, all
        };
        [Serializable]
        public class GameAsset
        {
            public GameObject assetPrefab;
            public Vector2 amount;
            public float radius;
            public ModuleType[] admissibleModules;
            public GameAsset(GameObject _assetPrefab, Vector2 _amount, float _radius, ModuleType[] _admissibleModules)
            {
                assetPrefab = _assetPrefab;
                amount = _amount;
                radius = _radius;
                admissibleModules = _admissibleModules;
            }
        }

        public GameAsset[] assetsToSpawn;
        private List<EnvironmentModule> activeModules = new List<EnvironmentModule>();

        public void RegisterModule(EnvironmentModule module)
        {
            activeModules.Add(module);
        }

        private void Start()
        {
            SpawnAssets();
        }

        private void SpawnAssets()
        {
            for (int i = 0; i < assetsToSpawn.Length; i++)
            {
                activeModules.Shuffle();
                int assetAmnt = UnityEngine.Random.Range((int)assetsToSpawn[i].amount.x, (int)assetsToSpawn[i].amount.y);
                for (int j = 0; j < assetAmnt; j++)
                {
                    int position = j;
                    while (Array.IndexOf(assetsToSpawn[i].admissibleModules, activeModules[position].moduleType) == -1)
                        position ++;
                    activeModules[position % activeModules.Count].SpawnAsset(assetsToSpawn[i]);
                }
            }
        }
    }
}
