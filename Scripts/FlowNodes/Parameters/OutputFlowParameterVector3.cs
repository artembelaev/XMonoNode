using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Let you using output parameters Vector3
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Output/Vector3", 0)]
    [CreateNodeMenu("Parameter/Output/Vector3", 0)]
    [NodeWidth(160)]
    public class OutputFlowParameterVector3 : OutputFlowParameter<Vector3>
    {
    }
}
