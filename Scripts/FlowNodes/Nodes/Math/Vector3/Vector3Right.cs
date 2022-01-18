using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Right", 28)]
    [NodeWidth(130)]
    public class Vector3Right : MonoNode
    {
        [Output] public Vector3 right;

        public override object GetValue(NodePort port)
        {
            return Vector3.right;
        }
    }
}
