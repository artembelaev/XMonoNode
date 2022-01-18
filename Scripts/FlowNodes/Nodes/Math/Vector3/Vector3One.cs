using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/One", 22)]
    [NodeWidth(130)]
    public class Vector3One : MonoNode
    {
        [Output] public Vector3 one;

        public override object GetValue(NodePort port)
        {
            return Vector3.one;
        }
    }
}
