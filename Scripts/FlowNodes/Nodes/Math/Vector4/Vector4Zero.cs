using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Zero", 21)]
    [NodeWidth(130)]
    public class Vector4Zero : MonoNode
    {
        [Output] public Vector4 zero;

        public override object GetValue(NodePort port)
        {
            return Vector4.zero;
        }
    }
}
