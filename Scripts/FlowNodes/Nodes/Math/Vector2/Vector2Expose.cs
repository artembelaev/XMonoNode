using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Expose", 2)]
    [NodeWidth(135)]
    public class Vector2Expose : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel, Inline]
        public Vector2      vector2;

        [Output] public float       x;
        [Output] public float       y;
        [Output] public Vector2     normalized;
        [Output] public float       magnitude;
        [Output] public float       sqrMagnitude;

        private NodePort vector2Port;
        private NodePort xPort;
        private NodePort yPort;
        private NodePort normalizedPort;
        private NodePort magnitudePort;
        private NodePort sqrMagnitudePort;

        protected override void Init()
        {
            base.Init();
            vector2Port     = GetInputPort(nameof(vector2));
            xPort           = GetOutputPort(nameof(x));
            yPort           = GetOutputPort(nameof(y));
            normalizedPort  = GetOutputPort(nameof(normalized));
            magnitudePort   = GetOutputPort(nameof(magnitude));
            sqrMagnitudePort= GetOutputPort(nameof(sqrMagnitude));
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            vector2 = vector2Port.GetInputValue(vector2);

            if (port == xPort)
            {
                return vector2.x;
            }
            else if (port == yPort)
            {
                return vector2.y;
            }
            else if (port == normalizedPort)
            {
                return vector2.normalized;
            }
            else if (port == magnitudePort)
            {
                return vector2.magnitude;
            }
            else if (port == sqrMagnitudePort)
            {
                return vector2.sqrMagnitude;
            }

            return null;
        }
    }
}
