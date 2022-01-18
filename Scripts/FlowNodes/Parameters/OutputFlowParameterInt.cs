using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Let you using output parameters int
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Output/int", 5)]
    [CreateNodeMenu("Parameter/Output/int", 5)]
    [NodeWidth(160)]
    public class OutputFlowParameterInt : OutputFlowParameter<int>
    {
    }
}
