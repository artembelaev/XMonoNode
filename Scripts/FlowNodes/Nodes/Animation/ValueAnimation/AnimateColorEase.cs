using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/AnimateValue/Color")]
    [NodeWidth(190)]
    public class AnimateColorEase : AnimateValue<Color>
    {
        protected override Color GetValue(float tNormal)
        {
            return from + (to - from) * tNormal;
        }
    }


}
