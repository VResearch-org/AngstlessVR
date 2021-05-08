using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VResearch.GameFlowControl.Environment
{
    abstract public class EnvironmentModule : MonoBehaviour
    {
        void Start()
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
    }
}
