using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Back", 24)]
    [NodeWidth(130)]
    public class Vector3Back : MonoNode
    {
        [Output] public Vector3 back;

        public override object GetValue(NodePort port)
        {
            return Vector3.back;
        }
    }
}
