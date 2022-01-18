using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ���������� Color, ���������� � ����� FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Input/Color", -1)]
    [CreateNodeMenu("Parameter/Input/Color", -1)]
    [NodeWidth(170)]
    public class InputFlowParameterColor : InputFlowParameter<Color>
    {
    }
}
