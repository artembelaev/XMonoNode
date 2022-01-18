﻿using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/" + nameof(OnButtonClicked))]
    public class OnButtonClicked : EventNode
    {
        [Input]
        public Button MyButton;

        // Use this for initialization
        protected override void Init()
        {
            base.Init();
            var thebutton = GetInputValue(nameof(MyButton), MyButton);
            thebutton?.onClick.AddListener(OnMyButtonClicked);
        }

        private void OnMyButtonClicked()
        {
            TriggerFlow();
        }

        private void OnDestroy()
        {
            var thebutton = GetInputValue(nameof(MyButton), MyButton);
            thebutton?.onClick.RemoveListener(OnMyButtonClicked);
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
