using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Up", 25)]
    [NodeWidth(120)]
    public class Vector2Up : MonoNode
    {
        [Output] public Vector2 up;

        public override object GetValue(NodePort port)
        {
            return Vector2.up;
        }
    }
}
