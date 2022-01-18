using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/AnimateValue/Vector3")]
    [NodeWidth(190)]
    public class AnimateVector3Ease : AnimateValue<Vector3>
    {
        private void Reset()
        {
            from = Vector3.zero;
            to = Vector3.forward;
        }

        protected override Vector3 GetValue(float tNormal)
        {
            return from + (to - from) * tNormal;
        }
    }


}
