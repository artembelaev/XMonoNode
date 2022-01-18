using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Располагает звук в точке
    /// </summary>
    [AddComponentMenu("X Sound Node/Position Binding", 205)]
    [CreateNodeMenu("Sound/Position Binding", 205)]
    [NodeWidth(180)]
    public class XSoundNodeVector3 : XSoundNodeSimpleOutput
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        public Vector3                    Vector3 = Vector3.zero;

        private void Reset()
        {
            Name = "Position Binding";
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(audioOutput))
            {
                Vector3 = GetInputValue(nameof(Vector3), Vector3);

                AudioSources sources = GetAudioInput();
                foreach (AudioSource source in sources.List)
                {
                    source.transform.position = Vector3;
                }
                return sources;
            }

            return null;
        }
    }
}
