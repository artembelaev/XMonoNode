using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Magnitude", 7)]
    [NodeWidth(150)]
    public class Vector4Magnitude : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4  vector4;

        [Output] public float   magnitude;

        private NodePort inputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(vector4));
        }

        public override object GetValue(NodePort port)
        {
            return inputPort.GetInputValue(vector4).magnitude;
        }
    }
}
