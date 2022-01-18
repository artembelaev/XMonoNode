using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XMonoNode;
using FlowNodesEditor;

namespace XMonoNodeEditor
{
    [CustomPropertyDrawer(typeof(PathToAudioClipAttribute))]
    public class PathToAudioClipAttributeDrawer : PropertyDrawer
    {
        private static AudioSource source = null;

        public static AudioSource Source
        {
            get
            {
                if (source == null)
                {
                    GameObject obj = new GameObject("AudioSource");
                    source = obj.AddComponent<AudioSource>();
                    obj.hideFlags = HideFlags.DontSave;
                }
                return source;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            if (property == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            int buttonWidth = 16;

            position.width -= buttonWidth - 2;

            EditorGUI.PropertyField(position, property, label);

            if (!string.IsNullOrEmpty(property.stringValue))
            {
                position.y += 1;
                position.x += position.width + 1;
                position.width = buttonWidth;
                position.height = buttonWidth;
                string path = "Sounds/" + property.stringValue;
                AudioClip clip = Resources.Load<AudioClip>(path);
                
                bool guiEnabled = GUI.enabled;
    
                GUI.enabled = clip != null;
                string tooltip = clip != null ? "play" : ("No audio clip at path: \"" + path + "\"");

                if (GUI.Button(position, new GUIContent("", tooltip), clip != null ? FlowNodeEditorResources.styles.playButton : FlowNodeEditorResources.styles.errorButton))
                {
                    if (!Source.isPlaying)
                    {
                        Source.gameObject.SetActive(true);
                        Source.PlayOneShot(clip);
                        Source.gameObject.name = "AudioSource: " + clip.name;
                    }
                    else
                    {
                        Source.Stop();
                        Source.gameObject.SetActive(false);
                    }
                }
                else if (!Source.isPlaying)
                {
                    Source.gameObject.SetActive(false);
                }

                GUI.enabled = guiEnabled;
            }

            EditorGUI.EndProperty();
        }
    }
}
