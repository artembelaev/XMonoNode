using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/GetTextMeshProUGUI", 417)]
    [NodeWidth(190)]
    public class GetTextMeshProUGUI : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Transform _transform;
        [Output]
        public TextMeshProUGUI text;

        private NodePort transformPort;

        protected override void Init()
        {
            base.Init();

            transformPort = GetInputPort(nameof(_transform));
        }

        private void Reset()
        {
            Name = "Get TextMeshProUGUI";
        }

        public override object GetValue(NodePort port)
        {
            Transform t = transformPort.GetInputValue(_transform);
            if (t == null)
            {
                return null;
            }

            var text = t.GetComponent<TextMeshProUGUI>();

            return text;
            
        }
    }
}
