using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Let you using output parameters Color
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Output/Color", -1)]
    [CreateNodeMenu("Parameter/Output/Color", -1)]
    [NodeWidth(170)]
    public class OutputFlowParameterColor : OutputFlowParameter<Color>
    {
    }
}
