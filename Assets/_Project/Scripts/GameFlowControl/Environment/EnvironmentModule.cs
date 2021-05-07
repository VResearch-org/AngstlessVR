using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnvironmentModule : MonoBehaviour
{
    // Start is called before the first frame update
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
