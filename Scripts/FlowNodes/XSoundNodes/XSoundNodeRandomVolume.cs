using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Изменяет свойство volume случайным образом
    /// </summary>
    [AddComponentMenu("X Sound Node/RandomVolume", 53)]
    [CreateNodeMenu("Sound/RandomVolume", 53)]
    [NodeWidth(150)]
    public class XSoundNodeRandomVolume : XSoundNodeRandomValue
    {
        protected override void Init()
        {
            base.Init();

            minValuePort.label = "Min Volume";
            maxValuePort.label = "Max Volume";
        }

        private void Reset()
        {
            Name = "Random Volume";
        }

        protected override void setRandomValue(float randomValue, AudioSources sources)
        {
            foreach (AudioSource source in sources.List)
            {
                if (source == null)
                    continue;

                source.volume = randomValue;
            }
        }
    }
}
