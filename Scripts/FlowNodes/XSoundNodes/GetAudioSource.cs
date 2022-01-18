using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Get AudioSource", 310)]
    [NodeWidth(210)]
    public class GetAudioSource : GetComponentBase<AudioSource>
    {
        protected override void Init()
        {
            base.Init();

            objPort.label = "Transform";
            componentPort.label = "AudioSource";
        }

        private void Reset()
        {
            Name = "Get AudioSource";
        }
    }
}
