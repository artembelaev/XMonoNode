using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Нода-источник звуков. Может воспроизводить soundId или заданные источники звуков
    /// </summary>
    [AddComponentMenu("X Sound Node/Id Source", 0)]
    [CreateNodeMenu("Sound/Id Source", 0)]
    [NodeWidth(240)]
    [NodeTint(70, 100, 70)]
    public class XSoundNodeSource : XSoundNodeBase
    {
        public XSoundNodeSource()
        {
        }

        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.Inherited)]
        public AudioSources audioOutput;

        [XSoundSelector]
        [SerializeField, HideLabel]
        private int                 soundId = -1;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public bool customParameters = false;

        [Input(connectionType: ConnectionType.Override), Hiding, Range(0f, 1f)]
        public float volume = 1f;

        [Input(connectionType: ConnectionType.Override), Hiding, Range(-3f, 3f)]
        public float pitch = 1f;

        private NodePort customParamsPort = null;
        private NodePort volumePort = null;
        private NodePort pitchPort = null;

        protected override void Init()
        {
            base.Init();
            
            customParamsPort = GetInputPort(nameof(customParameters));
            volumePort = GetInputPort(nameof(volume));
            pitchPort = GetInputPort(nameof(pitch));
        }

        private void Reset()
        {
            Name = "Id Source";
        }

        public override object GetValue(NodePort port)
        {
            if (this == null)
            {
                return new AudioSources();
            }

            if (audioOutput == null)
            {
                audioOutput = new AudioSources();
            }

            audioOutput.List.Clear();

            if (port == null || port.ConnectionCount == 0)
            {
                return audioOutput;
            }

            IXSoundsLibrary sounds = IXSoundsLibraryInstance.Get();

            if (sounds == null)
            {
                Debug.LogErrorFormat("IXSoundsLibraryInstance is null {0}.{1}", gameObject != null ? gameObject.name : "null", Name);
                return audioOutput;
            }

            if (soundId == -1)
            {
                Debug.LogErrorFormat(this, "Лёха!!! У ноды сурса звука id -1! {0} ({1})".Color(Color.magenta), gameObject != null ? gameObject.name : "null", Name);
                return audioOutput;
            }

            AudioSource source = sounds.Play(soundId, PlayParameters);
            
            if (source != null && customParamsPort != null)
            {
                if (customParamsPort.GetInputValue(customParameters) == true && volumePort != null && pitchPort != null)
                {
                    source.volume = volumePort.GetInputValue(volume);
                    source.pitch = pitchPort.GetInputValue(pitch);
                }

                source.transform.parent = transform != null ? transform.parent : null;
                source.transform.localPosition = Vector3.zero;

                audioOutput.List.Add(source);
            }
            else
            {
                Debug.LogErrorFormat("Attempt to play the sound '{0}' failed".Color(Color.magenta), name);
            }

            return audioOutput;
        }
    }
}
