using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Vector2To4", 41)]
    [NodeWidth(140)]
    public class Vector2To4 : MonoNode
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never)]
        public Vector2 vector2 = Vector2.zero;

        [Output(backingValue: ShowBackingValue.Never)]
        public Vector4 vector4;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(vector2));
        }

        public override object GetValue(NodePort port)
        {
            return (Vector4)inputPort.GetInputValue(vector2);
        }
    }
}
