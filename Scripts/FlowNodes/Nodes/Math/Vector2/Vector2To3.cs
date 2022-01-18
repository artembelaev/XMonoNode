using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Vector2To3", 41)]
    [NodeWidth(140)]
    public class Vector2To3 : MonoNode
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never)]
        public Vector2 vector2 = Vector2.zero;

        [Output(backingValue: ShowBackingValue.Never)]
        public Vector3 vector3;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(vector2));
        }

        public override object GetValue(NodePort port)
        {
            return (Vector3)inputPort.GetInputValue(vector2);
        }
    }
}
