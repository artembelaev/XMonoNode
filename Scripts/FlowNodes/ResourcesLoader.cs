using System;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    public static class ResourcesLoader
    {
        private static Dictionary<string, UnityEngine.Object>  dictionary = new Dictionary<string, UnityEngine.Object>();

        public static T Load<T>(string resourcePath) where T : UnityEngine.Object
        {
            if (dictionary.TryGetValue(resourcePath, out UnityEngine.Object result))
            {
                return (T)result;
            }
            else
            {
                result = Resources.Load<T>(resourcePath);

                if (result != null)
                {
                    dictionary[resourcePath] = result;
                    return (T)result;
                }
            }

            return default(T);
        }
        public static void Unload(string resourcePath)
        {
            UnityEngine.Object result = null;
            if (dictionary.TryGetValue(resourcePath, out result))
            {
                Resources.UnloadAsset(result);
                dictionary.Remove(resourcePath);
            }
        }

        public static void UnloadAll()
        {
            foreach (var pair in dictionary)
            {
                Resources.UnloadAsset(pair.Value);
            }
            dictionary.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}