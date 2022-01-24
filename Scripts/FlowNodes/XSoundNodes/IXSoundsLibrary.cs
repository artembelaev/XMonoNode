using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    public class IXSoundsLibraryInstance
    {
        public static string SoundLibraryResourcePath = "SystemInstantiator/Sounds";

        private static IXSoundsLibrary instance = null;
        

#if UNITY_EDITOR
        private static float            UpdateTimeSec = 5f;
        private static System.DateTime  lastUpdateTime = System.DateTime.Now;

        private static bool CanReloadByTime()
        {
            System.DateTime currentTime = System.DateTime.Now;
            bool result = (currentTime - lastUpdateTime).TotalSeconds > UpdateTimeSec;
            if (result)
            {
                lastUpdateTime = currentTime;
            }
            return result;
        }
#endif

        /// <summary>
        /// Use in your code to access IXSoundsLibrary singleton
        /// </summary>
        public static IXSoundsLibrary Get()
        {
            if (instance == null
#if UNITY_EDITOR
                ||
                (Application.isEditor == true && CanReloadByTime())
#endif
                )
            {
                GameObject obj = ResourcesLoader.Load<GameObject>(SoundLibraryResourcePath);
                if (obj != null)
                {
                    instance = obj.GetComponent<IXSoundsLibrary>();
                }
            }
            return instance;
        }

        /// <summary>
        /// Use in constructor of your singleton. By example:
        /// <code>
        /// IXSoundsLibraryInstance.set(this);
        /// </code>
        /// </summary>
        /// <param name="sounds"></param>
        public static void Set(IXSoundsLibrary sounds)
        {
            instance = sounds;
        }
    }

    public interface IXSoundsLibrary
    {
        public AudioSource Play(int soundId, params object[] parameters);

        public AudioSource Play(AudioClip clip);

        public Dictionary<int, string> GetSounds();
    }


    /// <summary>
    /// Property attribute to draw sound selection menu
    /// </summary>
    /// <see cref="IXSoundsLibrary.GetSounds()"/>
    public class XSoundSelectorAttribute : PropertyAttribute
    {
    }

}