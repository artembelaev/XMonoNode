using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Set CanvasGroup BlocksRaycasts", 428)]
    public class SetCanvasGroupBlocksRaycasts : SetObjectParameter<CanvasGroup, bool>
    {
        protected override void SetValue(CanvasGroup obj, bool value)
        {
            obj.blocksRaycasts = value;
        }
    }
}
