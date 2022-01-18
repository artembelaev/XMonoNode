using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Get CanvasGroup BlocksRaycasts", 427)]
    public class GetCanvasGroupBlocksRaycasts : GetObjectParameter<CanvasGroup, bool>
    {
        protected override bool GetValue(CanvasGroup obj)
        {
            return obj.blocksRaycasts;
        }
    }
}
