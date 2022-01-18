using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/GetMaterialInt", 473)]
    [NodeWidth(190)]
    public class GetMaterialInt : GetMaterialNamedParameter<int>
    {
        protected override int GetValue(Material obj)
        {
            return obj.GetInteger(NamePort.GetInputValue(paramName));
        }
    }
}
