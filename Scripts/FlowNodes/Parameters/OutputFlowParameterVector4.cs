using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Let you using output parameters Vector3
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Output/Vector4", 0)]
    [CreateNodeMenu("Parameter/Output/Vector4", 0)]
    [NodeWidth(160)]
    public class OutputFlowParameterVector4 : OutputFlowParameter<Vector4>
    {
    }
}
