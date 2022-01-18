using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/One", 22)]
    [NodeWidth(120)]
    public class Vector2One : MonoNode
    {
        [Output] public Vector2 one;

        public override object GetValue(NodePort port)
        {
            return Vector2.one;
        }
    }
}
