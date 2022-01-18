using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    public class PathToAudioClipAttribute : PropertyAttribute
    {
    }

    [System.Serializable]
    public class XAudioClipResource
    {
        public int ID;
        [PathToAudioClip]
        public string Path;

        public AudioClip Clip
        {
            get
            {
                if (clip == null)
                {
                    clip = ResourcesLoader.Load<AudioClip>("Sounds/" + Path);

                    if (clip == null)
                    {
                        Debug.LogErrorFormat("Clip '{0}' not found!".Color(Color.magenta), Path);
                    }
                }
                return clip;
            }
        }

        private AudioClip clip = null;
    }

    [ExecuteInEditMode]
    public class SimpleXSoundsLibrary : MonoBehaviour, IXSoundsLibrary
    {       
        [SerializeField]
        XAudioClipResource[] sounds = new XAudioClipResource[0];

        private List<AudioSource> sourcesPool = null;

        public Dictionary<int, string> GetSounds()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (var sound in sounds)
            {
                result[sound.ID] = sound.Path;
            }

            return result;
        }

        public AudioSource Play(int soundId, params object[] parameters)
        {
            if (!SoundsByID.TryGetValue(soundId, out XAudioClipResource sound))
            {
                Debug.LogErrorFormat(this, "Sound whith ID: {0} - not found! Most likely, you made a mistake in the file path".Color("magenta"), soundId);
                return null;
            }
            string name = GetName(sound.Clip);
            AudioSource source = GetSourceFromPool(name);

            source.clip = sound.Clip;
            source.loop = parameters.Get<bool>();
            source.transform.position = parameters.Get<Vector3>();

            source.volume = 1f;
            source.pitch = 1f;
            source.panStereo = 0f;
            source.spatialBlend = 0f;
            source.spatialBlend = 0f;
            source.reverbZoneMix = 1f;

            source.dopplerLevel = 1.0f;
            source.spread = 0f;
            source.minDistance = 1f;
            source.maxDistance = 500f;

            source.Play();

            if (Application.isPlaying && source.loop != true)
            {
                StartCoroutine(DisableSource(source));
            }

            return source;
        }

        private Dictionary<int, XAudioClipResource> soundsByID = null;
        protected Dictionary<int, XAudioClipResource> SoundsByID
        {
            get
            {
                if (soundsByID == null)
                {
                    soundsByID = new Dictionary<int, XAudioClipResource>();

                    for (int i = 0; i < sounds.Length; i++)
                    {
                        if (soundsByID.ContainsKey(sounds[i].ID))
                        {
                            continue;
                        }

                        soundsByID.Add(sounds[i].ID, sounds[i]);
                    }
                }

                return soundsByID;
            }
        }

        private string GetName(AudioClip clip)
        {
            string name = clip.name;
#if UNITY_EDITOR
            name = string.Format("AudioSource: {0}", name);
#endif
            return name;
        }

        private AudioSource CreateSource(string name)
        {
            AudioSource source = new GameObject(name).AddComponent<AudioSource>();
            source.playOnAwake = false;
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                source.gameObject.hideFlags = HideFlags.DontSave;
            }
#endif
            source.transform.SetParent(transform);
            return source;
        }

        private AudioSource GetSourceFromPool(string name)
        {
            if (sourcesPool == null)
            {
                sourcesPool = new List<AudioSource>();
            }

            AudioSource source = null;

            // Perhaps some resources in the pool have been removed - remove them
            int i = 0;
            while (source == null && i < sourcesPool.Count)
            {
                source = sourcesPool[i];
                ++i;
            }
            sourcesPool.RemoveRange(0, i);

            if (source == null)
            {
                source = CreateSource(name);
            }

            if (!source.gameObject.activeSelf)
            {
                source.gameObject.SetActive(true);
            }

            source.name = name;
            source.gameObject.name = name;

            source.volume = 1;

            return source;
        }

        public IEnumerator DisableSource(AudioSource source)
        {
            while (source != null && source.isPlaying)
            {
                yield return new UnityEngine.WaitForSeconds(1);
            }

            if (source != null && source.loop == false)
            {
                source.gameObject.SetActive(false);
                source.transform.SetParent(transform);
                sourcesPool.Add(source);
            }
        }

        private void OnEnable()
        {
            if (soundsByID != null)
            {
                soundsByID.Clear();
                soundsByID = null;
            }
            IXSoundsLibraryInstance.Set(this);
        }

        public AudioSource Play(AudioClip clip)
        {
            string name = GetName(clip);
            AudioSource source = GetSourceFromPool(name);

            source.clip = clip;
            source.Play();

            if (Application.isPlaying && source.loop != true)
            {
                StartCoroutine(DisableSource(source));
            }

            return source;
        }
    }
}
