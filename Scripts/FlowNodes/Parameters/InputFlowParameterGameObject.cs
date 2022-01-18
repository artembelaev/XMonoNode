using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ���������� GameObject, ���������� � ����� FlowNodeGraph.Flow()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Input/GameObject", 2)]
    [CreateNodeMenu("Parameter/Input/GameObject", 2)]
    [NodeWidth(220)]
    public class InputFlowParameterGameObject : InputFlowParameter<GameObject>
    {
    }
}
