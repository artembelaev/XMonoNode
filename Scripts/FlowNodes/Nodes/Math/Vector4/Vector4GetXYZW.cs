using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/GetXYZW", -6)]
    [NodeWidth(150)]
    public class Vector4GetXYZW : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4  a;

        [Output] public float   x;
        [Output] public float   y;
        [Output] public float   z;
        [Output] public float   w;

        private NodePort inputPort;

        private NodePort xPort;
        private NodePort yPort;
        private NodePort zPort;
        private NodePort wPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(a));

            xPort = GetOutputPort(nameof(x));
            yPort = GetOutputPort(nameof(y));
            zPort = GetOutputPort(nameof(z));
            wPort = GetOutputPort(nameof(w));
        }

        public override object GetValue(NodePort port)
        {
            if (port == xPort)
            {
                return inputPort.GetInputValue(a).x;
            }
            else if (port == yPort)
            {
                return inputPort.GetInputValue(a).y;
            }
            else if (port == zPort)
            {
                return inputPort.GetInputValue(a).z;
            }
            else if (port == wPort)
            {
                return inputPort.GetInputValue(a).w;
            }
            else
                return null;
        }
    }
}
