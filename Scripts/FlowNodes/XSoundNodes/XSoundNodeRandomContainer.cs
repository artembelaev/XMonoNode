using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Смешивает входящие звуки
    /// </summary>
    [AddComponentMenu("X Sound Node/Random Container", 303)]
    [CreateNodeMenu("Sound/Random Container", 303)]
    [NodeWidth(140)]
    public class XSoundNodeRandomContainer : XSoundNodesList
    {
        [Input(
            backingValue: ShowBackingValue.Unconnected,
            connectionType: ConnectionType.Override,
            typeConstraint: TypeConstraint.Inherited), Hiding]
        public int                      randomCount = 1; // unique random elements count

        private List<NodePort>          denyPorts = new List<NodePort>();

        private NodePort GetTrueRandom()
        {
            randomCount = GetInputValue("randomCount", randomCount);
            List<NodePort> inputs = GetAudioInputs();
            if (denyPorts.Count >= randomCount)
            {
                denyPorts.RemoveAt(0);
            }

            NodePort randomInputPort = inputs.RandomElement();
            while (denyPorts.Contains(randomInputPort))
            {
                randomInputPort = inputs.RandomElement();
            }
            denyPorts.Add(randomInputPort);
            return randomInputPort;
        }

        private void Reset()
        {
            Name = "Random";
            inputs.Add(new AudioSources());
            inputs.Add(new AudioSources());
            inputs.Add(new AudioSources());
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(output))
            {
                output = GetTrueRandom().GetInputValue<AudioSources>();
                return output;
            }
            else
            {
                return null;
            }
        }
    }
}
