using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/GetY", -8)]
    [NodeWidth(170)]
    public class Vector3GetY : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel, Inline]
        public Vector3  a;

        [Output, HideLabel] public float   y;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();
            inputPort = GetInputPort(nameof(a));
        }

        public override object GetValue(NodePort port)
        {
            return inputPort.GetInputValue(a).y;
        }
    }
}
