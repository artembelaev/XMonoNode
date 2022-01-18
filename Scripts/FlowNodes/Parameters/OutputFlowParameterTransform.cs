using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Let you using output parameters Transform
    /// </summary>
    [AddComponentMenu("FlowNode/Output/Parameter/Transform", 1)]
    [CreateNodeMenu("Parameter/Output/Transform", 1)]
    public class OutputFlowParameterTransform : OutputFlowParameter<Transform>
    {
    }
}
