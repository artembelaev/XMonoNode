using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Animator/Bool", 127)]
    public class AnimatorSetParamBool : AnimatorSetParam<bool> 
    {
        protected override void SetAnimatorValue(Animator animator, string name, bool value)
        {
            animator.SetBool(name, value);
        }
    }

}
