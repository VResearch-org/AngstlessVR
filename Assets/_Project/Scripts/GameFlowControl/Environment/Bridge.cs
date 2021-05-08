using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VResearch.GameFlowControl.Environment
{
    public class Bridge : EnvironmentModule
    {
        protected override Vector3 GetPosition(float radius)
        {
            Vector2 xz = Random.insideUnitCircle * ((dimensions.x / 2) - radius);
            return transform.position + new Vector3(xz.x, Random.Range(0, dimensions.y), xz.y);
        }
    }
}
