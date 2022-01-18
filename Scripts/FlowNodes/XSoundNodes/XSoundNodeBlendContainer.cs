using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Смешивает входящие звуки
    /// </summary>
    [AddComponentMenu("X Sound Node/Blend Container", 301)]
    [CreateNodeMenu("Sound/Blend Container", 301)]
    [NodeWidth(110)]
    public class XSoundNodeBlendContainer : XSoundNodesList
    {
        private void Reset()
        {
            Name = "Blend";
            inputs.Add(new AudioSources());
            inputs.Add(new AudioSources());
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(output))
            {
                List<NodePort> inputPorts = GetAudioInputs();
                var output = base.GetValue(port) as AudioSources;

                foreach (var inputPort in inputPorts)
                {
                    AudioSources sources = inputPort.GetInputValue<AudioSources>();
                    if (sources != null)
                    {
                        output.List.AddRange(sources.List);
                    }
                }

                return output;
            }
            else
            {
                return null;
            }
        }
    }
}
