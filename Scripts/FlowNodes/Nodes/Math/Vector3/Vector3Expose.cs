using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Expose", 2)]
    [NodeWidth(180)]
    public class Vector3Expose : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel, Inline]
        public Vector3      vector3;

        [Output] public float       x;
        [Output] public float       y;
        [Output] public float       z;
        [Output] public Vector3     normalized;
        [Output] public float       magnitude;
        [Output] public float       sqrMagnitude;

        private NodePort vector3Port;
        private NodePort xPort;
        private NodePort yPort;
        private NodePort zPort;
        private NodePort normalizedPort;
        private NodePort magnitudePort;
        private NodePort sqrMagnitudePort;

        protected override void Init()
        {
            base.Init();
            vector3Port     = GetInputPort(nameof(vector3));
            xPort           = GetOutputPort(nameof(x));
            yPort           = GetOutputPort(nameof(y));
            zPort = GetOutputPort(nameof(z));
            normalizedPort  = GetOutputPort(nameof(normalized));
            magnitudePort   = GetOutputPort(nameof(magnitude));
            sqrMagnitudePort= GetOutputPort(nameof(sqrMagnitude));
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            vector3 = vector3Port.GetInputValue(vector3);

            if (port == xPort)
            {
                return vector3.x;
            }
            else if (port == yPort)
            {
                return vector3.y;
            }
            else if (port == zPort)
            {
                return vector3.z;
            }
            else if (port == normalizedPort)
            {
                return vector3.normalized;
            }
            else if (port == magnitudePort)
            {
                return vector3.magnitude;
            }
            else if (port == sqrMagnitudePort)
            {
                return vector3.sqrMagnitude;
            }

            return null;
        }
    }
}
