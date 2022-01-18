using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Устанавливает миксер для звука
    /// </summary>
    [AddComponentMenu("X Sound Node/Mixer", 206)]
    [CreateNodeMenu("Sound/Mixer", 206)]
    [NodeWidth(200)]
    public class XSoundNodeMixer : XSoundNodeSimpleOutput
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        public AudioMixerGroup audioMixerGroup = null;

        private void Reset()
        {
            Name = "Mixer";
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(audioOutput))
            {
                audioMixerGroup = GetInputValue("audioMixerGroup", audioMixerGroup);

                AudioSources sources = GetAudioInput();
                foreach (AudioSource source in sources.List)
                {
                    if (source == null)
                        continue;

                    source.outputAudioMixerGroup = audioMixerGroup;
                }
                return sources;
            }

            return null;
        }
    }
}
