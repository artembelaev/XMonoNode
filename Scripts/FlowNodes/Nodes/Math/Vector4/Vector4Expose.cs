using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Expose", 2)]
    [NodeWidth(150)]
    public class Vector4Expose : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4      vector4;

        [Output] public float       x;
        [Output] public float       y;
        [Output] public float       z;
        [Output] public float       w;
        [Output] public Vector4     normalized;
        [Output] public float       magnitude;

        private NodePort vector4Port;
        private NodePort xPort;
        private NodePort yPort;
        private NodePort zPort;
        private NodePort wPort;
        private NodePort normalizedPort;
        private NodePort magnitudePort;

        protected override void Init()
        {
            base.Init();
            vector4Port     = GetInputPort(nameof(vector4));
            xPort           = GetOutputPort(nameof(x));
            yPort           = GetOutputPort(nameof(y));
            zPort           = GetOutputPort(nameof(z));
            wPort           = GetOutputPort(nameof(w));
            normalizedPort  = GetOutputPort(nameof(normalized));
            magnitudePort   = GetOutputPort(nameof(magnitude));
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            vector4 = vector4Port.GetInputValue(vector4);

            if (port == xPort)
            {
                return vector4.x;
            }
            else if (port == yPort)
            {
                return vector4.y;
            }
            else if (port == zPort)
            {
                return vector4.z;
            }
            else if (port == wPort)
            {
                return vector4.w;
            }
            else if (port == normalizedPort)
            {
                return vector4.normalized;
            }
            else if (port == magnitudePort)
            {
                return vector4.magnitude;
            }
            
            return null;
        }
    }
}
