using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Set CanvasGroup Alpha", 427)]
    public class SetCanvasGroupAlpha : SetObjectParameter<CanvasGroup, float>
    {
        protected override void SetValue(CanvasGroup obj, float value)
        {
            obj.alpha = value;
        }
    }
}
