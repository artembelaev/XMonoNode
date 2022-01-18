using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Left", 27)]
    [NodeWidth(130)]
    public class Vector3Left : MonoNode
    {
        [Output] public Vector3 left;

        public override object GetValue(NodePort port)
        {
            return Vector3.left;
        }
    }
}
