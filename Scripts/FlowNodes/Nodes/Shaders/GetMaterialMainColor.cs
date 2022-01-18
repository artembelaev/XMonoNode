using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/GetMaterialMainColor", 471)]
    [NodeWidth(190)]
    public class GetMaterialMainColor : GetObjectParameter<Material, Color>
    {
        protected override Color GetValue(Material obj)
        {
            return obj.color;
        }
    }
}
