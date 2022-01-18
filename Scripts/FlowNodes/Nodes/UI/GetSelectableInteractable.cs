using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Get Selectable Interactable", 431)]
    public class GetSelectableInteractable : GetObjectParameter<Selectable, bool>
    {
        protected override bool GetValue(Selectable obj)
        {
            return obj.interactable;
        }
    }
}
