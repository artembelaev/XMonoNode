using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    public class XSoundEventPlayer : MonoBehaviour
    {
        [SerializeField]
        private XSoundGetter        soundGetter = null;
        [SerializeField]
        private bool                playOnStart = true;
        [SerializeField]
        private bool                playOnEnable = false;

        [SerializeField]
        private bool                fadeOut = false;

        [SerializeField, Range(0f, 5f)]
        private float               fadeOutTime = 0.5f;

        public XSoundGetter SoundGetter => soundGetter;

        private XSoundNodeGraph     graph = null;
        private XSoundNodePlay      playNode = null;

        protected void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        protected void OnEnable()
        {
            if (playOnEnable)
            {
                Play();
            }
        }

        protected void OnDestroy()
        {
            FadeOut();
        }

        [ContextMenu("FadeOut")]
        public void FadeOut()
        {
            if (playNode != null && fadeOut && graph != null && graph.gameObject.activeInHierarchy)
            {
                graph.StartStaticCoroutine(FadeOut(fadeOutTime, playNode.PlayingSources().List));
            }
        }

        public void Play()
        {
            FlowNodeGraphContainer container = soundGetter.GetContainer();
            container.GraphParent = transform;
            graph = container.Flow(soundGetter.GraphId) as XSoundNodeGraph;
            if (graph != null)
            {
                playNode = graph.GetComponent<XSoundNodePlay>();
            }
        }

        public void Stop()
        {
            soundGetter.Stop();
        }

        private IEnumerator FadeOut(float time, List<AudioSource> sources)
        {
            if (Mathf.Approximately(time, 0))
            {
                playNode.Stop();
                yield break;
            }
            

            float fullTime = time;
            float[] volumes = new float[sources.Count];

            for (int i = 0; i < sources.Count; ++i)
            {
                if (sources[i] == null)
                {
                    continue;
                }
                volumes[i] = sources[i].volume;
            }

            while (time > 0f)
            {
                time -= (graph.UpdateMode == AnimatorUpdateMode.Normal ? Time.deltaTime : Time.unscaledDeltaTime);

                int i = 0;
                foreach (AudioSource source in sources)
                {
                    if (source == null)
                    {
                        continue;
                    }
                    source.volume = volumes[i] * time / fullTime;
                    ++i;
                }

                yield return null;
            }

            playNode.Stop();

        }

    }
}
