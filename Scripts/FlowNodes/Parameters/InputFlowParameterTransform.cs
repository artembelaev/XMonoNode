using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает Transform, переданный в метод FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Input/Parameter/Transform", 1)]
    [CreateNodeMenu("Parameter/Input/Transform", 1)]
    [NodeWidth(220)]
    public class InputFlowParameterTransform : InputFlowParameter<Transform>
    {
    }
}
