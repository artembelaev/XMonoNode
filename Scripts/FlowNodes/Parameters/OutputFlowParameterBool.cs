using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Let you using float output parameters
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Output/bool", 6)]
    [CreateNodeMenu("Parameter/Output/bool", 6)]
    [NodeWidth(140)]
    public class OutputFlowParameterBool : OutputFlowParameter<bool>
    {
    }
}
