using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает Vector4, переданный в метод FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Input/Vector4", 0)]
    [CreateNodeMenu("Parameter/Input/Vector4", 0)]
    [NodeWidth(160)]
    public class InputFlowParameterVector4 : InputFlowParameter<Vector4>
    {
    }
}
