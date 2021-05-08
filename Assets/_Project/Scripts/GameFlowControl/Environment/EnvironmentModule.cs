using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VResearch.GameFlowControl.Environment
{
    abstract public class EnvironmentModule : MonoBehaviour
    {
        public SpawnGameAssets.ModuleType moduleType;
        [SerializeField] protected Vector3 dimensions;

        protected virtual Vector3 GetPosition(float radius)
        {
            return transform.position + new Vector3(Random.Range(0 + radius, dimensions.x - radius) - dimensions.x / 2,
                Random.Range(0 + radius, dimensions.y - radius),
                Random.Range(0 + radius, dimensions.z - radius) - dimensions.z / 2);
        }

        void Awake()
        {
            RegisterModule();
            InitializeModule();
        }

        private void RegisterModule()
        {
            SpawnGameAssets.Instance.RegisterModule(this);
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
