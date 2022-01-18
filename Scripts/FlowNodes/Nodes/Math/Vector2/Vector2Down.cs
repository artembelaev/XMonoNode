using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Down", 26)]
    [NodeWidth(120)]
    public class Vector2Down : MonoNode
    {
        [Output] public Vector2 down;

        public override object GetValue(NodePort port)
        {
            return Vector2.down;
        }
    }
}
