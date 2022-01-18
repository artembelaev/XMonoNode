using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/SetMaterialColor", 479)]
    [NodeWidth(190)]
    public class SetMaterialColor : SetMaterialNamedParameter<Color>
    {
        protected override void SetValue(Material obj, Color value)
        {
            obj.SetColor(NamePort.GetInputValue(paramName), value);
        }
    }
}
