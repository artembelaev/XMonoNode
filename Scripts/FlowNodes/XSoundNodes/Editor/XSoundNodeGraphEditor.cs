using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XMonoNodeEditor;
using FlowNodesEditor;

namespace XMonoNode
{
    /// <summary>
    /// Окно звукового графа
    /// </summary>
    [CustomNodeGraphEditor(typeof(XSoundNodeGraph))]
    public class XSoundNodeGraphEditor : FlowNodeGraphGraphEditor
    {
        private FlowNodeGraph graph = null;

        private FlowNodeGraph Graph
        {
            get
            {
                if (graph == null)
                {
                    graph = Target as FlowNodeGraph;
                }
                return graph;
            }
        }

        public override void OnOpen()
        {
            window.titleContent = new GUIContent("Sound Node Graph", NodeEditorResources.graph);
        }

        public override void OnToolBarGUI()
        {
            if (GUILayout.Button(new GUIContent("Play"), EditorStyles.toolbarButton))
            {
                Graph.Flow();
            }

            if (GUILayout.Button(new GUIContent("Stop"), EditorStyles.toolbarButton))
            {
                Graph.Stop();
            }
            NodeEditorPreferences.GetSettings().flowPortButtons = GUILayout.Toggle(NodeEditorPreferences.GetSettings().flowPortButtons, "Buttons", EditorStyles.toolbarButton);
        }

        public override string GetPortTooltip(XMonoNode.NodePort port)
        {
            // Убираем вытягивание звуков при формировании подсказки, чтобы звуки не появлялись в сцене
            Type portType = port.ValueType;
            if (portType == typeof(List<AudioSource>) ||
                portType == typeof(AudioSources))
            {
                return portType.Name;
            }
            else
            {
                return base.GetPortTooltip(port);
            }
        }

        [MenuItem("GameObject/xMonoNode/SoundNodeGraph", false, 3)]
        public static void CreateXSoundNodeGraph()
        {
            GameObject current = Selection.activeGameObject;
            if (current != null)
            {
                var graph = current.AddComponent<XSoundNodeGraph>();
                NodeEditorWindow.Open(graph);
            }
        }

        [MenuItem("GameObject/xMonoNode/Simple Sound Library", false, 3)]
        public static void CreateSimpleXSoundsLibrary()
        {
            GameObject current = Selection.activeGameObject;
            if (current != null)
            {
                current.AddComponent<SimpleXSoundsLibrary>();
            }
        }

    }

    /// <summary>
    /// Инспектор звукового графа. Добавляет кнопки play, stop и т.д..
    /// </summary>
    [CustomEditor(typeof(XSoundNodeGraph), true)]
    public class XSoundNodeGraphInspector : FlowNodeGraphInspector
    {
        private XSoundNodeGraph soundNodeGraph = null;

        public override void OnInspectorGUI()
        {
            if (soundNodeGraph == null)
            {
                soundNodeGraph = target as XSoundNodeGraph;
            }

            base.OnInspectorGUI();

            

            // TODO playing information
        }

    }
}
