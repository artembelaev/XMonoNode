using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/SetMaterialVector", 478)]
    [NodeWidth(190)]
    public class SetMaterialVector : SetMaterialNamedParameter<Vector4>
    {
        protected override void SetValue(Material obj, Vector4 value)
        {
            obj.SetVector(NamePort.GetInputValue(paramName), value);
        }
    }
}
