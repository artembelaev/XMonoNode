using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Forward", 23)]
    [NodeWidth(130)]
    public class Vector3Forward : MonoNode
    {
        [Output] public Vector3 forward;

        public override object GetValue(NodePort port)
        {
            return Vector3.forward;
        }
    }
}
