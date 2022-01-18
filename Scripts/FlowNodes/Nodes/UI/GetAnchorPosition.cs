using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/GetAnchorPosition", 409)]
    [NodeWidth(200)]
    public class GetAnchorPosition : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public RectTransform rectTransform;

        [Output] public Vector2 anchorPosition;

        private NodePort rectTransformPort;

        private void Reset()
        {
            Name = "Get Anchor Position";
        }

        protected override void Init()
        {
            base.Init();

            rectTransformPort = GetInputPort(nameof(rectTransform));
        }

        public override object GetValue(NodePort port)
        {
            return rectTransformPort.GetInputValue(rectTransform);
        }
    }
}
