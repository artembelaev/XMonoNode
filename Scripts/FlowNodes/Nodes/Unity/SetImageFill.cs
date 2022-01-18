using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/" + nameof(SetImageFill), "Sprite")]
    public class SetImageFill : FlowNodeInOut
    {
        [Input] public Image Target;
        [Input] public float Fill;

        public override void Flow(NodePort flowPort)
        {
            var target = GetInputValue(nameof(Target), Target);
            var fillAmount = GetInputValue(nameof(Fill), Fill);
            target.fillAmount = fillAmount;
            FlowOut();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}
