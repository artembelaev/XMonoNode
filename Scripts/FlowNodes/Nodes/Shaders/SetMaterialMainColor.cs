using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/SetMaterialMainColor", 472)]
    [NodeWidth(190)]
    public class SetMaterialMainColor : SetObjectParameter<Material, Color>
    {
        protected override void SetValue(Material obj, Color value)
        {
            obj.color = value;
        }
    }
}
