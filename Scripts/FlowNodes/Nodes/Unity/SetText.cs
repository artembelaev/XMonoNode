using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/" + nameof(SetText), "Set", "Text")]
    public class SetText : FlowNodeInOut
    {
        [Input] public Text Target;
        [Input] public string Text;

        public override void Flow(NodePort flowPort)
        {
            var target = GetInputValue(nameof(Target), Target);
            var text = GetInputValue<object>(nameof(Text), Text);
            target.text = $"{text}";
            FlowOut();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}
