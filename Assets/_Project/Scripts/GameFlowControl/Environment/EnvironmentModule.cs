using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VResearch.GameFlowControl.Environment
{
    abstract public class EnvironmentModule : MonoBehaviour
    {
        void Awake()
        {
            RegisterModule();
            InitializeModule();
        }

        private void RegisterModule()
        {
            SpawnGameAssets.Instance.RegisterModule(this);
        }

        protected virtual Vector3 GetPosition(float radius)
        {
            return Vector3.zero;
        }

        protected virtual void InitializeModule()
        {
        }

        public virtual void SpawnAsset(SpawnGameAssets.GameAsset gameAsset)
        {
            Instantiate(gameAsset.assetPrefab, GetPosition(gameAsset.radius), Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }
}
