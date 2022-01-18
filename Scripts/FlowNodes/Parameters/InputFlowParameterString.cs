using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает string, переданный в метод XSoundNodeGraph.Play()
    /// </summary>
    [AddComponentMenu("FlowNode/Parameter/Input/string", 7)]
    [CreateNodeMenu("Parameter/Input/string", 7)]
    public class InputFlowParameterString : InputFlowParameter<string>
    {
    }
}
