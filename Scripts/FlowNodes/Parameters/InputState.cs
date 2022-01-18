using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ¬озвращает параметр state, переданный в метод FLowNodeGraph.Flow(Action<string> onEndAction, string state)
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/State", 6)]
    [CreateNodeMenu("Parameter/State", 6)]
    [NodeTint(50, 105, 70)]
    [NodeWidth(80)]
    public class InputState : MonoNode
    {
        [Output(backingValue: ShowBackingValue.Never)]
        public string   State;

        private void Reset()
        {
            Name = "State";
        }

        public override object GetValue(NodePort port)
        {
            FlowNodeGraph flowGraph = graph as FlowNodeGraph;
            if (flowGraph != null)
            {
                return flowGraph.State;
            }

            return "";
        }
    }
}
