using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Возвращает угол между осью oz камеры и инаправлением от камеры на источник звука
    /// </summary>
    [AddComponentMenu("X Sound Node/ListenerAngle", 225)]
    [CreateNodeMenu("Sound/ListenerAngle", 225)]
    [NodeWidth(150)]
    public class XSoundNodeListenerAngle: XSoundNodeSimple
    {
        [Output]
        public float  listenerAngle;
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Hiding]
        public bool normalized = true;

        private void Reset()
        {
            Name = "Listener Angle";
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(listenerAngle))
            {
                AudioSources sources = GetAudioInput();

                float angle = 0.0f;
                if (sources.List.Count != 0)
                {
                    AudioSource source = sources.List[0];
                    Transform cameraTransform = Camera.main.transform;
                    angle = Vector3.Angle(source.transform.position - cameraTransform.position, cameraTransform.forward);
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
