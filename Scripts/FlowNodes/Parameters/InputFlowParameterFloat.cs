using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает float, переданный в метод FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Input/float", 4)]
    [CreateNodeMenu("Parameter/Input/float", 4)]
    [NodeWidth(160)]
    public class InputFlowParameterFloat : InputFlowParameter<float>
    {
        private void Reset()
        {
            Name = "Parameter: float"; // в оригинале получается single
        }
    }
}
