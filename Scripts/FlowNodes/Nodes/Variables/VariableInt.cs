using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XMonoNode
{

    [AddComponentMenu("FlowNode/Variables/int", 6)]
    [CreateNodeMenu("Variables/int", 6)]
    public class VariableInt : VariableNode<int>
    {
        private void Reset()
        {
            Name = "Variable: int"; // в оригинале получается Int32
        }
    }
}



