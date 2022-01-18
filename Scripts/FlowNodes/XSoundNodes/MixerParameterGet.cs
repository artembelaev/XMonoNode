using UnityEngine;
using UnityEngine.Audio;

namespace XMonoNode
{
    [CreateNodeMenu("Sound/MixerParameterGet", 10011)]
    public class MixerParameterGet : GetObjectParameter<AudioMixer, float>
    {
        [Input(connectionType: ConnectionType.Override)]
        public string exposedParam;

        private NodePort exposedParamPort = null;

        protected override void Init()
        {
            base.Init();
            exposedParamPort = GetInputPort(nameof(exposedParam));
        }

        protected override float GetValue(AudioMixer obj)
        {
            string param = exposedParamPort.GetInputValue(exposedParam);
            if (obj.GetFloat(param, out float value))
            {
                return value;
            }
            else
            {
                Debug.LogErrorFormat("Mixer \"{0}\" hasn't parameter \"{1}\"", obj.name, param);
                return 0f;
            }
        }
    }
}
