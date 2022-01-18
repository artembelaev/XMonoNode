using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Left", 27)]
    [NodeWidth(120)]
    public class Vector2Left : MonoNode
    {
        [Output] public Vector2 left;

        public override object GetValue(NodePort port)
        {
            return Vector2.left;
        }
    }
}
