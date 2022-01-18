using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/LocalRotation", 103)]
    public class TweenLocalRotation : TweenVector3Transform
    {
        [SerializeField]
        private bool modAngle = false;

        private void Reset()
        {
            Name = "Local Rotation";
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

            target.localEulerAngles = value;
        }

        protected override Vector3 GetStartValue()
        {
            if (target == null)
            {
                return Vector3.one;
                
            }

            return target.localEulerAngles;
        }

        protected override void SetValue(Vector3 value)
        {
            if (target == null)
            {
                return;
            }

            target.localEulerAngles = value;
        }
    }
}
