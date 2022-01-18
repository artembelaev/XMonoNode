using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Изменяет свойство pitch случайным образом
    /// </summary>
    [AddComponentMenu("X Sound Node/RandomPitch", 54)]
    [CreateNodeMenu("Sound/RandomPitch", 54)]
    [NodeWidth(150)]
    public class XSoundNodeRandomPitch : XSoundNodeRandomValue
    {
        protected override void Init()
        {
            base.Init();

            minValuePort.label = "Min Pitch";
            maxValuePort.label = "Max Pitch";
        }

        private void Reset()
        {
            Name = "Random Pitch";
        }

        protected override void setRandomValue(float randomValue, AudioSources sources)
        {
            foreach (AudioSource source in sources.List)
            {
                if (source == null)
                    continue;

                source.pitch = randomValue;
            }
        }
    }
}
