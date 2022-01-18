using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/AnchoredPosition", 111)]
    public class TweenAnchoredPosition : TweenVector2RectTransform
    {

        private void Reset()
        {
            Name = "Anchored Position";
        }

        protected override Vector2 GetStartValue()
        {
            if (target == null)
            {
                return Vector3.zero;
            }

            return target.anchoredPosition;
        }

        protected override void SetValue(Vector2 value)
        {
            if (target == null)
            {
                return;
            }

            target.anchoredPosition = value;
        }
    }
}
