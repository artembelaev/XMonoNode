using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/SetMaterialFloat", 476)]
    [NodeWidth(190)]
    public class SetMaterialFloat : SetMaterialNamedParameter<float>
    {
        protected override void SetValue(Material obj, float value)
        {
            obj.SetFloat(NamePort.GetInputValue(paramName), value);
        }
    }
}
