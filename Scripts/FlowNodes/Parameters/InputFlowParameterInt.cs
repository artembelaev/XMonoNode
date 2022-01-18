using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает int, переданный в метод FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Input/int", 5)]
    [CreateNodeMenu("Parameter/Input/int", 5)]
    [NodeWidth(160)]
    public class InputFlowParameterInt : InputFlowParameter<int>
    {
    }
}
