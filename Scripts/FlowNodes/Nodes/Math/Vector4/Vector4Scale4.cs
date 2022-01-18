using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Scale4", 6)]
    [NodeWidth(150)]
    public class Vector4Scale4 : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4  vector4;

        [Input(connectionType: ConnectionType.Override)]
        public Vector4  scale = Vector4.one;

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
            vector4 = vector4Port.GetInputValue(vector4);
            Vector3 result = vector4;
            result.Scale(scalePort.GetInputValue(scale));
            return result;
        }
    }
}
