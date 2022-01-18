using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XMonoNodeEditor;

namespace XMonoNode
{
    [CustomPropertyDrawer(typeof(XSoundSelectorAttribute))]
    public class XSoundSelectorDrawer: PropertyDrawer
    {
        private string[]  values = null;

        protected List<int> identificators = null;

        protected void AddItem(int id, string value)
        {
            values[identificators.Count] = value;
            identificators.Add(id);
        }

        private void Init(Dictionary<int, string> soundsDict)
        {
            if (values != null && values.Length > 0)
            {
                return;
            }

            values = new string[soundsDict.Count + 1];
            identificators = new List<int>(values.Length);

            AddItem(-1, "-: None");

            foreach (var pair in soundsDict)
            {
               AddItem(pair.Key, pair.Value + ": " + pair.Key);
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property == null)
            {
                return;
            }
            
            Init(IXSoundsLibraryInstance.Get().GetSounds());

            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Calculate rects
            Rect pathRect = new Rect(position.x + 0 * position.width / 2 + 0 * 4, position.y, position.width - 6, position.height);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            int intValue = property.intValue;
            

            if (NodeEditorWindow.current != null &&
                NodeEditorWindow.current.zoom >= 0.9f && NodeEditorWindow.current.zoom <= 1.3f)
            {
                int popupValue = EditorGUI.Popup(pathRect, Mathf.Max(0, identificators.IndexOf(intValue)), values);
                intValue = identificators[popupValue];
                property.intValue = intValue;
            }
            else
            {
                GUI.Label(pathRect, new GUIContent(values[Mathf.Max(0, identificators.IndexOf(intValue))]));
            }
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }

    [CustomNodeEditor(typeof(XSoundNodeFadeIn))]
    public class XSoundNodeFadeInEditor : NodeEditor
    {
        public override int OnBodyGUI()
        {
            int propertyCount = base.OnBodyGUI();
            ++propertyCount;
            if (Target.ShowState == INode.ShowAttribState.ShowAll)
            {
                XSoundNodeFadeIn node = target as XSoundNodeFadeIn;

                node.EasingMode = (EasingMode)EditorGUILayout.EnumPopup(new GUIContent(ObjectNames.NicifyVariableName(nameof(XSoundNodeFadeOut.EasingMode))), node.EasingMode);

                Texture2D tex = FlowNodeEditorResources.EaseTextureClamped01(node.EasingMode, false);

                GUILayout.BeginHorizontal();
                GUILayout.Label("", GUILayout.ExpandWidth(true), GUILayout.MinWidth(50));
                GUILayout.Label(new GUIContent(tex), GUILayout.MinWidth(tex.width + 2), GUILayout.Height(tex.height + 2));
                GUILayout.EndHorizontal();
            }
            else
            {
                NodeEditorGUILayout.hasHiddenProperty = true;
            }
            return propertyCount;
        }
    }

    [CustomNodeEditor(typeof(XSoundNodeFadeOut))]
    public class XSoundNodeFadeOutEditor : NodeEditor
    {
        public override int OnBodyGUI()
        {
            int propertyCount = base.OnBodyGUI();
            ++propertyCount;
            if (Target.ShowState == INode.ShowAttribState.ShowAll)
            {
                XSoundNodeFadeOut node = target as XSoundNodeFadeOut;

                node.EasingMode = (EasingMode)EditorGUILayout.EnumPopup(new GUIContent(ObjectNames.NicifyVariableName(nameof(XSoundNodeFadeOut.EasingMode))), node.EasingMode);

                Texture2D tex = FlowNodeEditorResources.EaseTextureClamped01(node.EasingMode, true);

                GUILayout.BeginHorizontal();
                GUILayout.Label("", GUILayout.ExpandWidth(true), GUILayout.MinWidth(50));
                GUILayout.Label(new GUIContent(tex), GUILayout.MinWidth(tex.width + 2), GUILayout.Height(tex.height + 2));
                GUILayout.EndHorizontal();
            }
            else
            {
                NodeEditorGUILayout.hasHiddenProperty = true;
            }
            return propertyCount;
        }
    }
}
