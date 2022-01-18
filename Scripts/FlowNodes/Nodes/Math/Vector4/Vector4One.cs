using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/One", 22)]
    [NodeWidth(130)]
    public class Vector4One : MonoNode
    {
        [Output] public Vector4 one;

        public override object GetValue(NodePort port)
        {
            return Vector4.one;
        }
    }
}
