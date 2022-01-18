using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ���������� Vector3, ���������� � ����� FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Input/Vector3", 0)]
    [CreateNodeMenu("Parameter/Input/Vector3", 0)]
    [NodeWidth(180)]
    public class InputFlowParameterVector3 : InputFlowParameter<Vector3>
    {
    }
}
