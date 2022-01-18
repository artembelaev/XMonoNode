using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Normalized", 8)]
    [NodeWidth(150)]
    public class Vector34Normalized : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4  vector4;

        [Output] public Vector3 normalized;

        private NodePort inputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(vector4));
        }

        public override object GetValue(NodePort port)
        {
            return inputPort.GetInputValue(vector4).normalized;
        }
    }
}
