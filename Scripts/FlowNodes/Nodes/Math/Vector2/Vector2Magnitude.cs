using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Magnitude", 7)]
    [NodeWidth(135)]
    public class Vector2Magnitude : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2  vector2;

        [Output] public float   magnitude;

        private NodePort inputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(vector2));
        }

        public override object GetValue(NodePort port)
        {
            return inputPort.GetInputValue(vector2).magnitude;
        }
    }
}
