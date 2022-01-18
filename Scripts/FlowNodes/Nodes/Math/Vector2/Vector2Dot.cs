using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Dot", 7)]
    [NodeWidth(130)]
    public class Vector2Dot : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2  a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2  b;

        [Output] public Vector2 dot;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(dot))
            {
                a = GetInputValue(nameof(a), a);
                var result = a;
                result.Scale(GetInputValue(nameof(b), b));
                return Vector2.Dot(GetInputValue(nameof(a), a), GetInputValue(nameof(b), b));
            }

            return null;
        }
    }
}
