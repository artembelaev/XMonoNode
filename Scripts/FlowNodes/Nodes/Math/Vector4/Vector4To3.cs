using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Vector4To3", 44)]
    [NodeWidth(140)]
    public class Vector4To3 : MonoNode
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never)]
        public Vector4 vector4 = Vector4.zero;

        [Output(backingValue: ShowBackingValue.Never)]
        public Vector3 vector3;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();
            inputPort = GetInputPort(nameof(vector4));
        }

        public override object GetValue(NodePort port)
        {
            return (Vector3)inputPort.GetInputValue(vector4);
        }
    }
}
