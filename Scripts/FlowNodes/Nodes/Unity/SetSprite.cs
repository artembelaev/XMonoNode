using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/" + nameof(SetSprite))]
    public class SetSprite : FlowNodeInOut
    {
        [Input] public SpriteRenderer Target;
        [Input] public Sprite MySprite;

        public override void Flow(NodePort flowPort)
        {
            var target = GetInputValue(nameof(Target), Target);
            var sprite = GetInputValue(nameof(MySprite), MySprite);
            target.sprite = sprite;
            FlowOut();
        }

        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}