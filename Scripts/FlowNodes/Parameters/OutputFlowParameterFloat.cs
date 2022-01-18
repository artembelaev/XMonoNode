using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Let you using output parameters float
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Output/float", 4)]
    [CreateNodeMenu("Parameter/Output/float", 4)]
    [NodeWidth(160)]
    public class OutputFlowParameterFloat : OutputFlowParameter<float>
    {
        private void Reset()
        {
            Name = "Output: float"; // в оригинале получается single
        }
    }
}
