using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XMonoNode;

namespace XMonoNodeEditor
{
    //[CustomPropertyDrawer(typeof(Flow), true)]
    public class FlowDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            base.OnGUI(position, property, label);

            if (property == null)
            {
                return;
            }
            
            XMonoNode.INode node = property.serializedObject.targetObject as XMonoNode.INode;

            if (node == null)
            {
                return;
            }
            XMonoNode.NodePort port = node.GetPort(property.name);
            if (port == null)
            {
                return;
            }

           // Debug.Log(port.label + " " + NodeEditorUtilities.PortButtonPressedCount());

            if (port.direction == NodePort.IO.Output && NodeEditorUtilities.GetPortButtonPressed(port))
            {
                Debug.Log(port.label);
                FlowUtils.FlowOutput(port);
            }
        }

    }

    [CustomPropertyDrawer(typeof(Range<float>))]
    public class FloatRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            position.width /= 2;
            position.width -= 1;

            float min = property.FindPropertyRelative("min").floatValue;
            float max = property.FindPropertyRelative("max").floatValue;

            min = EditorGUI.FloatField(position, new GUIContent(""), min);
            position.x += position.width + 2;
            max = EditorGUI.FloatField(position, new GUIContent(""), max);

            property.FindPropertyRelative("min").floatValue = min;
            property.FindPropertyRelative("max").floatValue = max;

            EditorGUI.EndProperty();
        }
    }

    [CustomPropertyDrawer(typeof(Range<int>))]
    public class IntRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            position.width /= 2;
            position.width -= 1;

            int min = property.FindPropertyRelative("min").intValue;
            int max = property.FindPropertyRelative("max").intValue;

            min = EditorGUI.IntField(position, new GUIContent(""), min);
            position.x += position.width + 2;
            max = EditorGUI.IntField(position, new GUIContent(""), max);

            property.FindPropertyRelative("min").intValue = min;
            property.FindPropertyRelative("max").intValue = max;

            EditorGUI.EndProperty();
        }
    }

}
