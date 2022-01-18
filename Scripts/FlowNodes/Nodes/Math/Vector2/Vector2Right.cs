using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Right", 28)]
    [NodeWidth(120)]
    public class Vector2Right : MonoNode
    {
        [Output] public Vector2 right;

        public override object GetValue(NodePort port)
        {
            return Vector2.right;
        }
    }
}
