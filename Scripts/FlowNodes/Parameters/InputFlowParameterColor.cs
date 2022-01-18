using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает Color, переданный в метод FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Input/Color", -1)]
    [CreateNodeMenu("Parameter/Input/Color", -1)]
    [NodeWidth(170)]
    public class InputFlowParameterColor : InputFlowParameter<Color>
    {
    }
}
