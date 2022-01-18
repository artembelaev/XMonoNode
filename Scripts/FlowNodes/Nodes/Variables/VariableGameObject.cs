using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XMonoNode
{
    [AddComponentMenu("FlowNode/Variables/GameObject", 3)]
    [CreateNodeMenu("Variables/GameObject", 3)]
    [NodeWidth(240)]
    public class VariableGameObject : VariableNode<GameObject>
    {
    }
}



