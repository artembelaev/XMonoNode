using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/GetX", -9)]
    [NodeWidth(170)]
    public class Vector3GetX : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel, Inline]
        public Vector3  a;

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
