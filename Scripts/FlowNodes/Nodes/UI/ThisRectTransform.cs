using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/ThisRectTransform", 404)]
    [NodeWidth(140)]
    public class ThisRectTransform : MonoNode
    {
        [Output] public RectTransform output;

        private UnityEngine.UI.Graphic graphic = null;

        private UnityEngine.UI.Graphic GetGraphic()
        {
            if (graphic == null)
            {
                graphic = GetComponent<UnityEngine.UI.Graphic>();
            }
            return graphic;
        }
        protected override void Init()
        {
            base.Init();
        }

        private void Reset()
        {
            Name = "This RectTransform";
        }

        public override object GetValue(NodePort port)
        {
            UnityEngine.UI.Graphic graphic = GetGraphic();
            if (graphic != null && port.fieldName == nameof(output))
            {
                return graphic.rectTransform;
            }
            return null;
        }
    }
}
