#if DOTWEEN_SUPPORTED
using DG.Tweening;
using XMonoNode;

namespace XMonoNode
{
    public abstract class BaseDoTween : FlowNodeInOut
    {
        [Input(connectionType: ConnectionType.Override)]
        public float Duration = 1;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public float DelaySeconds = 0;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public int LoopsAmount;


        [NodeEnum, Hiding]
        public LoopType Loop;

        [NodeEnum]
        public Ease Easing = Ease.Linear;

        protected Tweener tween;

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }

        protected Tweener SetupTween(Tweener toSetup)
        {
            toSetup.SetDelay(DelaySeconds)
                .SetEase(Easing)
                .SetLoops(LoopsAmount, Loop);
            toSetup.onComplete += OnTweenComplete;
            return toSetup;
        }

        protected void OnTweenComplete()
        {
            FlowOut();
            tween = null;
        }
    }
}
#endif
