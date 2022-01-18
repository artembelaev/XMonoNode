using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XMonoNode;
using XMonoNodeEditor;

namespace FlowNodesEditor
{
    /// <summary>
    /// Окно графа
    /// </summary>
    [CustomNodeGraphEditor(typeof(FlowNodeGraph))]
    public class FlowNodeGraphGraphEditor : NodeGraphEditor
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
            base.OnOpen();
            window.titleContent = new GUIContent("Flow Node Graph", NodeEditorResources.graph);
        }

        [MenuItem("GameObject/xMonoNode/FlowNodeGraph", false, 2)]
        public static void CreateFlowNodeGraph()
        {
            GameObject current = Selection.activeGameObject;
            if (current != null)
            {
                var graph = current.AddComponent<FlowNodeGraph>();
                NodeEditorWindow.Open(graph);
            }
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (Graph == null)
            {
                return;
            }

            INode[] nodes = Graph.GetNodes();

            foreach (var node in nodes)
            {
                var ports = node.Ports;
                foreach (NodePort port in ports)
                {
                    if (port != null /*&& port.ValueType == typeof(Flow)*/ && NodeEditorUtilities.GetPortButtonPressed(port))
                    {
                        FlowUtils.Flow(port);
                    }
                }
            }
        }

        public override void addCustomContextMenuItemsForPort(GenericMenu contextMenu, NodePort hoveredPort)
        {
            if (hoveredPort.ValueType == typeof(Flow))
            {
                contextMenu.AddItem(new GUIContent("Flow"), false, () => FlowUtils.Flow(hoveredPort));
            }
        }

        public override void OnToolBarGUI()
        {

            if (GUILayout.Button(new GUIContent("Flow"), EditorStyles.toolbarButton))
            {
                Graph.Flow();
            }

            if (GUILayout.Button(new GUIContent("Stop"), EditorStyles.toolbarButton))
            {
                Graph.Stop();
            }

            NodeEditorPreferences.GetSettings().flowPortButtons = GUILayout.Toggle(NodeEditorPreferences.GetSettings().flowPortButtons, "Buttons", EditorStyles.toolbarButton);
        }

        public override float GetNoodleThickness(NodePort output, NodePort input)
        {
            float coef =  1.0f;
            if (output != null && output.ValueType == typeof(Flow) ||
                input != null && input.ValueType == typeof(Flow))
            {
                coef = 2.0f;
            }

            return NodeEditorPreferences.GetSettings().noodleThickness * coef;
        }

        public override GUIStyle GetPortStyle(NodePort port)
        {
            if (port.ValueType != typeof(Flow) && !NodeEditorUtilities.GetCachedAttrib(port, out FlowPortAttribute attr))
            {
                return base.GetPortStyle(port);
            }
            
            if (port.direction == NodePort.IO.Input)
                return FlowNodeEditorResources.styles.inputPortFlow;

            return FlowNodeEditorResources.styles.outputPortFlow;
        }

        public override string GetPortTooltip(NodePort port)
        {
            // Убираем вытягивание звуков при формировании подсказки, чтобы звуки не появлялись в сцене
            Type portType = port.ValueType;
            if (portType == typeof(Flow))
            {
                return (port.direction == NodePort.IO.Input ? "Input " : "Output ") + portType.Name + ": " + (string.IsNullOrEmpty(port.label) ? ObjectNames.NicifyVariableName(port.fieldName) : port.label);
            }
            else
            {
                return base.GetPortTooltip(port);
            }
        }

        public override void OnDropObjects(UnityEngine.Object[] objects)
        {
            Vector2 posDelta = new Vector2(32, 32);
            Vector2 pos = window.WindowToGridPosition(window.LastMousePosition);
            foreach (UnityEngine.Object obj in objects)
            {
                if (obj is GameObject)
                {
                    GameObject gameObject = obj as GameObject;

                    if (gameObject.TryGetComponent(out FlowNodeGraph refGraph))
                    {
                        InvokeGraphByRef refNode = CreateNode(typeof(InvokeGraphByRef), pos) as InvokeGraphByRef;
                        refNode.graphPrefab = refGraph;

                        pos += posDelta;
                    }
                }
            }
        }

    }

    /// <summary>
    /// Инспектор графа. Добавляет кнопки execute, stop и т.д..
    /// </summary>
    [CustomEditor(typeof(FlowNodeGraph), true)]
    public class FlowNodeGraphInspector : MonoNodeInspector
    {
        private FlowNodeGraph flowNodeGraph = null;

        public override void OnInspectorGUI()
        {
            if (flowNodeGraph == null)
            {
                flowNodeGraph = target as FlowNodeGraph;
            }
            base.OnInspectorGUI();

            if (flowNodeGraph == null)
            {
                GUILayout.Label(new GUIContent("flowNodeGraph is null".Color(Color.red)));
                return;
            }

           // GUILayout.Label(new GUIContent("<color=green>=== Test ===</color>", "test float parameter of Execute()"), GUIStyle.none);

            // Start/Stop buttons

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Flow", GUILayout.Height(40)))
            {
                if (Application.isPlaying == false)
                {
                    OpenGraph();
                }
                flowNodeGraph.Flow();
            }
            if (GUILayout.Button("Stop", GUILayout.Height(40)))
            {
                flowNodeGraph.Stop();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
        }
    }
}
