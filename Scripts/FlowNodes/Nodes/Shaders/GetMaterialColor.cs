using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Shaders/GetMaterialColor", 479)]
    [NodeWidth(190)]
    public class GetMaterialColor : GetMaterialNamedParameter<Color>
    {
        protected override Color GetValue(Material obj)
        {
            return obj.GetVector(NamePort.GetInputValue(paramName));
        }
    }
}
