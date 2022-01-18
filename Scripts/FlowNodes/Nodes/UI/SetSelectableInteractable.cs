using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Set Selectable Interactable", 432)]
    public class SetSelectableInteractable : SetObjectParameter<Selectable, bool>
    {
        protected override void SetValue(Selectable obj, bool value)
        {
            obj.interactable = value;
        }
    }
}
