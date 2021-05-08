using UnityEngine;

namespace VResearch.GameFlowControl.Environment
{
    public class UtilityTunnel : EnvironmentModule
    {
        [SerializeField] protected Vector3 dimensions;

        protected override Vector3 GetPosition(float radius)
        {
            return transform.position + new Vector3(Random.Range(0 + radius, dimensions.x - radius) - dimensions.x / 2,
                Random.Range(0 + radius, dimensions.y - radius),
                Random.Range(0 + radius, dimensions.z - radius) - dimensions.z / 2);
        }
    }
}
