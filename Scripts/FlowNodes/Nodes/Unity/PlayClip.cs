using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Sound/Play Clip", 10001)]
    [NodeWidth(180)]
    public class PlayClip : FlowNodeInOut 
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), HideLabel]
        public AudioClip Audio;

        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Hiding]
        public Vector3 TargetPosition;

        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Hiding]
        public float Volume = 1;

        private void Reset()
        {
            Name = "Play Clip";
        }

        public override void Flow(NodePort flowPort) 
        {
            var audio = GetInputValue(nameof(Audio), Audio);
            var position = GetInputValue(nameof(TargetPosition), TargetPosition);
            var volume = GetInputValue(nameof(Volume), Volume);
            AudioSource.PlayClipAtPoint(audio, position, volume);
            FlowOut();
        }

        public override object GetValue(NodePort port) 
        {
            return null;
        }
    }
}
