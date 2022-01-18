using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ¬озвращает угол между осью oz источника звука инаправлением от истчника звука на камеру
    /// </summary>
    [AddComponentMenu("X Sound Node/SourceAngle", 224)]
    [CreateNodeMenu("Sound/SourceAngle", 224)]
    [NodeWidth(150)]
    public class XSoundNodeGetSourceAngle: XSoundNodeSimple
    {
        [Output]
        public float  sourceAngle;
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Hiding]
        public bool normalized = true;

        private void Reset()
        {
            Name = "Source Angle";
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(sourceAngle))
            {
                AudioSources sources = GetAudioInput();

                float angle = 0.0f;
                if (sources.List.Count != 0)
                {
                    AudioSource source = sources.List[0];
                    Transform cameraTransform = Camera.main.transform;
                    angle = Vector3.Angle(source.transform.position - cameraTransform.position, source.transform.forward);
                    if (GetInputValue(nameof(normalized), normalized))
                    {
                        angle /= 180.0f;
                    }
                }
                return angle;
            }

            return null;
        }
    }
}
