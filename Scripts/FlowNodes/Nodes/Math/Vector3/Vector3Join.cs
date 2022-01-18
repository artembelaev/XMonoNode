using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Join", 1)]
    [NodeWidth(80)]
    public class Vector3Join : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public float x;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public float y;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public float z;

        [Output] public Vector3 vector3;

        private NodePort xPort;
        private NodePort yPort;
        private NodePort zPort;

        private void Reset()
        {
            Name = "Vect3 Join";
        }

        protected override void Init()
        {
            base.Init();
            xPort = GetInputPort(nameof(x));
            yPort = GetInputPort(nameof(y));
            zPort = GetInputPort(nameof(z));
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return new Vector3(xPort.GetInputValue(x), yPort.GetInputValue(y), zPort.GetInputValue(z));
        }
    }
}
