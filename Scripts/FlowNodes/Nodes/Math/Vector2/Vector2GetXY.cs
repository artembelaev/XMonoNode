using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/GetXY", -6)]
    [NodeWidth(140)]
    public class Vector2GetXY : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel, Inline]
        public Vector2  a;

        [Output] public float   x;
        [Output] public float   y;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();
            inputPort = GetInputPort(nameof(a));
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(x))
            {
                return inputPort.GetInputValue(a).x;
            }
            else //if (port.fieldName == nameof(y))
            {
                return inputPort.GetInputValue(a).y;
            }

        }
    }
}
