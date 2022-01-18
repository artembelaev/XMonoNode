using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Utils/Button", 522)]
    [NodeWidth(150)]
    public class ButtonNode : EventNode
    {
        [SerializeField, HideInNodeInspector]
        private string buttonText = "Press";

        public string ButtonText
        {
            get => buttonText;
            set => buttonText = value;
        }

        private void Reset()
        {
            Name = "Button";
        }

        protected override void Init()
        {
            base.Init();
            
        }

    }
}
