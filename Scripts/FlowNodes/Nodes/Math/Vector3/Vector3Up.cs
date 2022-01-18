using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Up", 25)]
    [NodeWidth(130)]
    public class Vector3Up : MonoNode
    {
        [Output] public Vector3 up;

        public override object GetValue(NodePort port)
        {
            return Vector3.up;
        }
    }
}
