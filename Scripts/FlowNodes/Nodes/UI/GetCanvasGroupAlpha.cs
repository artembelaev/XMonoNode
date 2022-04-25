using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Get CanvasGroup Alpha", 426)]
    public class GetCanvasGroupAlpha : GetObjectParameter<CanvasGroup, float>
    {
        protected override float GetValue(CanvasGroup obj)
        {
#if UNITY_EDITOR
            if(obj != null)
            {
#endif
                return obj.alpha;

#if UNITY_EDITOR
            }
            else
            {
                return 0;
            }
#endif
        }
    }
}
