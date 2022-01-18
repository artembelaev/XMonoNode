using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Get CanvasGroup Interactable", 425)]
    public class GetCanvasGroupInteractable : GetObjectParameter<CanvasGroup, bool>
    {
        protected override bool GetValue(CanvasGroup obj)
        {
            return obj.interactable;
        }
    }
}
