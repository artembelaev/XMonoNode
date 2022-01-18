using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает float, переданный в метод FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/At", 14)]
    [CreateNodeMenu("Parameter/Input/At", 14)]
    [NodeTint(50, 70, 105)]
    [NodeWidth(160)]
    public class InputFlowParameterAt : InputFlowParameter
    {
        [Input]
        public int at = 0;

        [Output] public string value;

        private void Reset()
        {
            Name = "Parameter: At"; // в оригинале получается single
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(value))
            {
                return GetDefaultValue();
            }
            return null;
        }

        public override object GetDefaultValue()
        {
            FlowNodeGraph flowGraph = graph as FlowNodeGraph;
            if (flowGraph != null && at >= 0 && at < flowGraph.FlowParametersArray.Length)
            {
                return flowGraph.FlowParametersArray[at];
            }
            else
                return null;
        }
    }
}
