#if DOTWEEN_SUPPORTED
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XMonoNode;


namespace XMonoNode
{
    [CreateNodeMenu("Animation/DoTween/Alpha", 94)]
    public class DoTweenAlpha : BaseDoTween
    {
        [Input] public Graphic Target;
        [Input] public float TargetValue;
        private Material objectMaterial;

        public override void Flow(NodePort flowPort)
        {
            StartTween(GetInputValue(nameof(TargetValue), TargetValue));
        }

        public void StartTween(float targetValue)
        {
            if (tween == null)
            {
                Target = GetInputValue(nameof(Target), Target);
                if (Target != null)
                {
                    objectMaterial = new Material(Target.material);
                    Target.material = objectMaterial;

                    var duration = GetInputValue(nameof(Duration), Duration);
                    tween = objectMaterial.DOFade(targetValue, duration);
                    SetupTween(tween);
                }
            }
        }
    }
}
#endif
