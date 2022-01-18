using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/" + nameof(SetImage), "Set", "Image", "Sprite")]
    public class SetImage : FlowNodeInOut
    {
        [Input] public Image Target;
        [Input] public Sprite MySprite;

        public override void Flow(NodePort flowPort)
        {
            var target = GetInputValue(nameof(Target), Target);
            var sprite = GetInputValue(nameof(MySprite), MySprite);
            target.sprite = sprite;
            FlowOut();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}
