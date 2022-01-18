using UnityEngine;
using UnityEngine.Audio;

namespace XMonoNode
{
    [CreateNodeMenu("Sound/MixerParameterSet", 10012)]
    public class MixerParameterSet : SetObjectParameter<AudioMixer, float>
    {
        [Input(connectionType: ConnectionType.Override)]
        public string exposedParam;

        private NodePort exposedParamPort = null;

        protected override void Init()
        {
            base.Init();
            exposedParamPort = GetInputPort(nameof(exposedParam));
        }

        protected override void SetValue(AudioMixer obj, float value)
        {
            string param = exposedParamPort.GetInputValue(exposedParam);
            if (!obj.SetFloat(param, value))
            {
                Debug.LogErrorFormat("Mixer \"{0}\" hasn't parameter \"{1}\"", obj.name, param);
            }
        }
    }
}
