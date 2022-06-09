using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Logic/On Switcher", 103)]
    [NodeWidth(120)]
    public class OnSwitcher : SwitcherBase, IUpdatable
    {
        public void OnUpdate(float deltaTime)
        {
            if (!output && inputPort.GetInputValue(input))
            {
                output = true;
            }
        }
    }
}