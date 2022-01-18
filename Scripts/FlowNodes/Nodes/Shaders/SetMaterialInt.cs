using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/SetMaterialInt", 474)]
    [NodeWidth(190)]
    public class SetMaterialInt : SetMaterialNamedParameter<int>
    {
        protected override void SetValue(Material obj, int value)
        {
            obj.SetInteger(NamePort.GetInputValue(paramName), value);
        }
    }
}
