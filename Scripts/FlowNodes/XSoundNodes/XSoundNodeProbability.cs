using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ¬оспроизводит звук с заданной веро€тностью
    /// </summary>
    [AddComponentMenu("X Sound Node/Probability", 103)]
    [CreateNodeMenu("Sound/Probability", 103)]
    [NodeWidth(160)]
    public class XSoundNodeProbability : XSoundNodeSimpleOutput
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        [Range(0.0f, 1.0f)]
        public float                    probability = 1.0f;

        private void Reset()
        {
            Name = "Probability";
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(audioOutput))
            {
                probability = GetInputValue(nameof(probability), probability);

                if (Random.Range(0f, 1f) <= probability)
                {
                    return GetAudioInput();
                }
                else
                {
                    return new AudioSources();
                }
            }

            return null;
        }
    }
}
