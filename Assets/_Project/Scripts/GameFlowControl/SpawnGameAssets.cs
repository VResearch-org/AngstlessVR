using System;
using System.Collections.Generic;
using UnityEngine;
using VResearch.GameFlowControl.Environment;

namespace VResearch.GameFlowControl
{
    public class SpawnGameAssets : Singleton<SpawnGameAssets>
    {
        [Serializable]
        public class GameAsset
        {
            public GameObject assetPrefab;
            public Vector2 amount;
            public GameAsset(GameObject _assetPrefab, Vector2 _amount)
            {
                assetPrefab = _assetPrefab;
                amount = _amount;
            }
        }

        public GameAsset[] assetsToSpawn;
        private List<EnvironmentModule> activeModules = new List<EnvironmentModule>();

        public void RegisterModule(EnvironmentModule module)
        {
            activeModules.Add(module);
        }
    }
}
