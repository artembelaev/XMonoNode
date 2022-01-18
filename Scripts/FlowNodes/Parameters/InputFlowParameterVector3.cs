using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает Vector3, переданный в метод FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Input/Vector3", 0)]
    [CreateNodeMenu("Parameter/Input/Vector3", 0)]
    [NodeWidth(180)]
    public class InputFlowParameterVector3 : InputFlowParameter<Vector3>
    {
    }
}
