using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Rotation", 104)]
    public class TweenRotation : TweenVector3Transform
    {
        [SerializeField]
        private bool modAngle = false;

        private void Reset()
        {
            Name = "Rotation";
        }

        protected override void OnTweenTick(float tNormal)
        {
            if (target == null)
            {
                return;
            }

            Vector3 value = modAngle ?
                new Vector3(
                Mathf.LerpAngle(startValue.x, targetValue.x, tNormal),
                Mathf.LerpAngle(startValue.y, targetValue.y, tNormal),
                Mathf.LerpAngle(startValue.z, targetValue.z, tNormal))
                :
                Vector3.Lerp(startValue, targetValue, tNormal);

            target.eulerAngles = value;
        }

        protected override Vector3 GetStartValue()
        {
            if (target == null)
            {
                return Vector3.one;
                
            }

            return target.eulerAngles;
        }

        protected override void SetValue(Vector3 value)
        {
            if (target == null)
            {
                return;
            }

            target.eulerAngles = value;
        }
    }
}
