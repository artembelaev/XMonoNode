using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Vector4To2", 42)]
    [NodeWidth(140)]
    public class Vector4To2 : MonoNode
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never)]
        public Vector4 vector4 = Vector4.zero;

        [Output(backingValue: ShowBackingValue.Never)]
        public Vector2 vector2;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(vector4));
        }

        public override object GetValue(NodePort port)
        {
            return (Vector2)inputPort.GetInputValue(vector4);
        }
    }
}
