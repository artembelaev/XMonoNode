using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Vector3To2", 42)]
    [NodeWidth(140)]
    public class Vector3To2 : MonoNode
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never)]
        public Vector3 vector3 = Vector3.zero;

        [Output(backingValue: ShowBackingValue.Never)]
        public Vector2 vector2;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(vector3));
        }

        public override object GetValue(NodePort port)
        {
            return (Vector2)inputPort.GetInputValue(vector3);
        }
    }
}
