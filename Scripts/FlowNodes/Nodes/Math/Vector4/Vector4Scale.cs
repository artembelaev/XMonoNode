using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Scale", 5)]
    [NodeWidth(150)]
    public class Vector4Scale : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4  vector4;

        [Input(connectionType: ConnectionType.Override)]
        public float    scale;

        [Output] public Vector4 scaled;

        private NodePort vector4Port;
        private NodePort scalePort;

        protected override void Init()
        {
            base.Init();

            vector4Port = GetInputPort(nameof(vector4));
            scalePort = GetInputPort(nameof(scale));
        }

        public override object GetValue(NodePort port)
        {
            return vector4Port.GetInputValue(vector4) * scalePort.GetInputValue(scale);
        }
    }
}
