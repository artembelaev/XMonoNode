using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/GetMaterialFloat", 475)]
    [NodeWidth(190)]
    public class GetMaterialFloat : GetMaterialNamedParameter<float>
    {
        protected override float GetValue(Material obj)
        {
            return obj.GetFloat(NamePort.GetInputValue(paramName));
        }
    }
}
