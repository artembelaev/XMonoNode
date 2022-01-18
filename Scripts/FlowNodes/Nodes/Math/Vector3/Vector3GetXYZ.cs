using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/GetXYZ", -6)]
    [NodeWidth(180)]
    public class Vector3GetXYZ : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel, Inline]
        public Vector3  a;

        [Output] public float   x;
        [Output] public float   y;
        [Output] public float   z;

        private NodePort inputPort;

        private NodePort xPort;
        private NodePort yPort;
        private NodePort zPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(a));

            xPort = GetOutputPort(nameof(x));
            yPort = GetOutputPort(nameof(y));
            zPort = GetOutputPort(nameof(z));
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
            else
                return null;
        }
    }
}
