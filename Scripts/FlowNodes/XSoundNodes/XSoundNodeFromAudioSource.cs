using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Нода-источник звуков, которая создаёт звуки по префабу
    /// </summary>
    [CreateNodeMenu("Sound/Audio Source", 2)]
    [NodeWidth(180)]
    [NodeTint(70, 100, 70)]
    public class XSoundNodeFromAudioSource : XSoundNodeBase
    {
        private List<AudioSource> sources = new List<AudioSource>();

        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.Inherited)]
        public AudioSources audioOutput;

        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.InheritedAny), HideLabel]
        public AudioSource source;

        private void Reset()
        {
            Name = "Audio Source";
        }

        private AudioSource GetAudioSource()
        {
            AudioSource original = GetInputValue(nameof(source), source);

            if (original == null)
            {
                return null;
            }

            AudioSource newSource = sources.Find(source => source != null && !source.isPlaying);

            if (newSource == null)
            {
                newSource = new GameObject(string.Format("AudioSource: {0}", original.gameObject.name)).AddComponent<AudioSource>();//ComponentUtils.CopyComponent(original, new GameObject()));
                sources.Add(newSource);
#if UNITY_EDITOR
                if (Application.isEditor)
                {
                    newSource.gameObject.hideFlags = HideFlags.DontSave;
                }
#endif
                sources.Add(newSource);
            }

            newSource.clip = original.clip;
            newSource.volume = original.volume;
            newSource.loop = original.loop;
            newSource.outputAudioMixerGroup = original.outputAudioMixerGroup;

            newSource.dopplerLevel = original.dopplerLevel;
            newSource.spread = original.spread;
            newSource.maxDistance = original.maxDistance;
            newSource.rolloffMode = original.rolloffMode;
            newSource.pitch = original.pitch;

            newSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, original.GetCustomCurve(AudioSourceCurveType.CustomRolloff));
            newSource.SetCustomCurve(AudioSourceCurveType.Spread, original.GetCustomCurve(AudioSourceCurveType.Spread));
            newSource.SetCustomCurve(AudioSourceCurveType.SpatialBlend, original.GetCustomCurve(AudioSourceCurveType.SpatialBlend));
            newSource.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, original.GetCustomCurve(AudioSourceCurveType.ReverbZoneMix));
            newSource.spatialBlend = original.spatialBlend;

            return newSource;
        }

        public override object GetValue(NodePort port)
        {
            AudioSource source = GetAudioSource();
            if (source != null)
            {
                return new AudioSources(new List<AudioSource>() { source });
            }
            else
            {
                return new AudioSources();
            }
        }
    }
}
