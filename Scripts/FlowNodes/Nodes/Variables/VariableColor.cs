using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XMonoNode
{
    [AddComponentMenu("FlowNode/Variables/Color", 3)]
    [CreateNodeMenu("Variables/Color", 3)]
    [NodeWidth(200)]
    public class VariableColor : VariableNode<Color>
    {
    }
}



