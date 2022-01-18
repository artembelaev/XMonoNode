using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/GetX", -9)]
    [NodeWidth(150)]
    public class Vector4GetX : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4  a;

        [Output] public float   x;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();
            inputPort = GetInputPort(nameof(a));
        }

        public override object GetValue(NodePort port)
        {
            return inputPort.GetInputValue(a).x;
        }
    }
}
