using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Logic/On-Off Switcher", 104)]
    [NodeWidth(120)]
    public class OnOffSwitcher : SwitcherBase, IUpdatable
    {
        public void OnUpdate(float deltaTime)
        {
            bool newInput = inputPort.GetInputValue(input);
            if (newInput && !input)
            {
                output = !output;
            }
            input = newInput;
        }

        public override void Flow(NodePort flowPort)
        {
            output = false;
            input = false;
        }
    }
}