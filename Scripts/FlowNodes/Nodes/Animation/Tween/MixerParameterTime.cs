using UnityEngine;
using UnityEngine.Audio;

namespace XMonoNode
{
    [CreateNodeMenu("Sound/MixerParameterTime", 10014)]
    public class MixerParameterTime : TweenFloat<AudioMixer>
    {
        [Input(connectionType: ConnectionType.Override)]
        public string exposedParam;

        private NodePort exposedParamPort = null;

        protected override void Init()
        {
            base.Init();
            exposedParamPort = GetInputPort(nameof(exposedParam));
        }

        protected override float GetStartValue()
        {
            string param = exposedParamPort.GetInputValue(exposedParam);
            bool ok = target.GetFloat(param, out float value);
            
            if (!ok)
            {
                Debug.LogErrorFormat("Mixer \"{0}\" hasn't parameter \"{1}\"", target.name, param);
            }

            return value;
        }

        protected override void SetValue(float value)
        {
            string param = exposedParamPort.GetInputValue(exposedParam);
            target.SetFloat(param, value);
        }

    }
}
