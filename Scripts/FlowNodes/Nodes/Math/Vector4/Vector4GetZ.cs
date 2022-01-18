using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/GetZ", -7)]
    [NodeWidth(150)]
    public class Vector4GetZ : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4  a;

        [Output] public float   z;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();
            inputPort = GetInputPort(nameof(a));
        }

        public override object GetValue(NodePort port)
        {
            return inputPort.GetInputValue(a).z;
        }
    }
}
