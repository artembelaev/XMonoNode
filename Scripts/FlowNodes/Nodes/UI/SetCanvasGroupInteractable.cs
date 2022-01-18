using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Set CanvasGroup Interactable", 426)]
    public class SetCanvasGroupInteractable : SetObjectParameter<CanvasGroup, bool>
    {
        protected override void SetValue(CanvasGroup obj, bool value)
        {
            obj.interactable = value;
        }
    }
}
