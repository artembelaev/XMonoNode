using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Zero", 21)]
    [NodeWidth(130)]
    public class Vector3Zero : MonoNode
    {
        [Output] public Vector3 zero;

        public override object GetValue(NodePort port)
        {
            return Vector3.zero;
        }
    }
}
