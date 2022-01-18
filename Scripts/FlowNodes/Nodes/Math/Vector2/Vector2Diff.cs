using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Diff", 4)]
    [NodeWidth(135)]
    public class Vector2Diff : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2 a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2 b;

        [Output] public Vector2 difference;

        protected override void Init()
        {
            base.Init();

            NodePort portIn = GetOutputPort(nameof(difference));
            if (portIn != null)
            {
                portIn.label = "A - B";
            }
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(difference))
            {

                return GetInputValue(nameof(a), a) - GetInputValue(nameof(b), b);
            }

            return null; // Replace this
        }
    }
}
