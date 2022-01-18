using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XMonoNode
{
    [AddComponentMenu("FlowNode/Variables/float", 5)]
    [CreateNodeMenu("Variables/float", 5)]
    public class VariableNodeFloat : VariableNode<float>
    {
        private void Reset()
        {
            Name = "Variable: float"; // в оригинале получается single
        }
    }
}



