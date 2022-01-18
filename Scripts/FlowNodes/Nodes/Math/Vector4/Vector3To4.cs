using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Vector3To4", 43)]
    [NodeWidth(140)]
    public class Vector3To4 : MonoNode
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never)]
        public Vector3 vector3 = Vector3.zero;

        [Output(backingValue: ShowBackingValue.Never)]
        public Vector4 vector4;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(vector3));
        }

        public override object GetValue(NodePort port)
        {
            return (Vector4)inputPort.GetInputValue(vector3);
        }
    }
}
