using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Let you using output parameters GameObject
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Output/GameObject", 2)]
    [CreateNodeMenu("Parameter/Output/GameObject", 2)]
    public class OutputFlowParameterGameObject : OutputFlowParameter<GameObject>
    {
    }
}
