using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Distance", 10)]
    [NodeWidth(130)]
    public class Vector2Distance : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2 a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2 b;

        [Output] public float distance;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(distance))
            {
                return Vector2.Distance(GetInputValue(nameof(a), a), GetInputValue(nameof(b), b));
            }

            return null; // Replace this
        }
    }
}
