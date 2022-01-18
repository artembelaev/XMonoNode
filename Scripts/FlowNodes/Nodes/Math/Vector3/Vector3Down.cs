using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Down", 26)]
    [NodeWidth(130)]
    public class Vector3Down : MonoNode
    {
        [Output] public Vector3 down;

        public override object GetValue(NodePort port)
        {
            return Vector3.down;
        }
    }
}
