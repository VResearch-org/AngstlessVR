using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VResearch.GameFlowControl.Environment
{
    public class Habitat : EnvironmentModule
    {
        protected override Vector3 GetPosition(float radius)
        {
            return RotateTransformAroundPivot(transform.position + new Vector3(Random.Range(0 + radius, dimensions.x - radius),
                Random.Range(0 + radius, dimensions.y - radius),
                Random.Range(0 + radius, dimensions.z - radius) - dimensions.z / 2), this.transform.position, this.transform.localEulerAngles);
        }

        private Vector3 RotateTransformAroundPivot(Vector3 currentTransform, Vector3 pivot, Vector3 angles)
        {
            Vector3 dir = currentTransform - pivot; // get point direction relative to pivot
            dir = Quaternion.Euler(angles) * dir; // rotate it
            currentTransform = dir + pivot; // calculate rotated point
            return currentTransform; // return it
        }
    }
}
