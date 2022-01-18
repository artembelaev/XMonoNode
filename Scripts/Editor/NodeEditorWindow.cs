using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace XMonoNodeEditor {
    [InitializeOnLoad]
    public partial class NodeEditorWindow : EditorWindow {
        public static NodeEditorWindow current;
        
        /// <summary> Stores node positions for all nodePorts. </summary>
        public Dictionary<XMonoNode.NodePort, Rect> portConnectionPoints { get { return _portConnectionPoints; } }
        private Dictionary<XMonoNode.NodePort, Rect> _portConnectionPoints = new Dictionary<XMonoNode.NodePort, Rect>();
        [SerializeField] private NodePortReference[] _references = new NodePortReference[0];
        [SerializeField] private Rect[] _rects = new Rect[0];

        private Func<bool> isDocked {
            get {
                if (_isDocked == null) _isDocked = this.GetIsDockedDelegate();
                return _isDocked;
            }
        }
        private Func<bool> _isDocked;

        [System.Serializable] private class NodePortReference {
            [SerializeField] private Object _node;
            [SerializeField] private string _name;

            public NodePortReference(XMonoNode.NodePort nodePort) {
                _node = nodePort.node as UnityEngine.Object;
                _name = nodePort.fieldName;
            }

            public XMonoNode.NodePort GetNodePort() {
                if (_node == null) {
                    return null;
                }
                return (_node as XMonoNode.INode).GetPort(_name);
            }
        }

        private void OnDisable() {
            // Cache portConnectionPoints before serialization starts
            int count = portConnectionPoints.Count;
            _references = new NodePortReference[count];
            _rects = new Rect[count];
            int index = 0;
            foreach (var portConnectionPoint in portConnectionPoints) {
                _references[index] = new NodePortReference(portConnectionPoint.Key);
                _rects[index] = portConnectionPoint.Value;
                index++;
            }
        }

        private void OnEnable()
        {
            // Reload portConnectionPoints if there are any
            int length = _references.Length;
            if (length == _rects.Length) {
                for (int i = 0; i < length; i++) {
                    if (_references[i] == null)
                        continue;
                    XMonoNode.NodePort nodePort = _references[i].GetNodePort();
                    if (nodePort != null)
                    {
                        _portConnectionPoints[nodePort] = _rects[i];
                    }
                }
            }
        }

        public Dictionary<XMonoNode.INode, Vector2> nodeSizes { get { return _nodeSizes; } }
        private Dictionary<XMonoNode.INode, Vector2> _nodeSizes = new Dictionary<XMonoNode.INode, Vector2>();
        public UnityEngine.Object graph;
        public Vector2 panOffset { get { return _panOffset; } set { _panOffset = value; Repaint(); } }
        private Vector2 _panOffset;
        public float zoom { get { return _zoom; } set { _zoom = Mathf.Clamp(value, NodeEditorPreferences.GetSettings().minZoom, NodeEditorPreferences.GetSettings().maxZoom); Repaint(); } }
        private float _zoom = 1;

        void OnFocus() {
            current = this;
            ValidateGraphEditor();
            if (graphEditor != null) {
                graphEditor.OnWindowFocus();
                if (NodeEditorPreferences.GetSettings().autoSave) AssetDatabase.SaveAssets();
            }
            
            dragThreshold = Math.Max(1f, Screen.width / 1000f);
        }
        
        void OnLostFocus() {
            if (graphEditor != null) graphEditor.OnWindowFocusLost();
        }

        [InitializeOnLoadMethod]
        private static void OnLoad() {
            Selection.selectionChanged -= OnSelectionChanged;
            Selection.selectionChanged += OnSelectionChanged;
        }

        /// <summary> Handle Selection Change events</summary>
        private static void OnSelectionChanged() {
            XMonoNode.INodeGraph nodeGraph = Selection.activeObject as XMonoNode.INodeGraph;
            if (nodeGraph != null && !AssetDatabase.Contains(nodeGraph as UnityEngine.Object)) {
                if (NodeEditorPreferences.GetSettings().openOnCreate) Open(nodeGraph);
            }
        }

        /// <summary> Make sure the graph editor is assigned and to the right object </summary>
        private void ValidateGraphEditor() {
            NodeGraphEditor graphEditor = NodeGraphEditor.GetEditor(graph as XMonoNode.INodeGraph, this);
            if (this.graphEditor != graphEditor && graphEditor != null) {
                this.graphEditor = graphEditor;
                graphEditor.OnOpen();
            }
        }

        /// <summary> Create editor window </summary>
        public static NodeEditorWindow Init() {
            NodeEditorWindow w = CreateInstance<NodeEditorWindow>();
            w.titleContent = new GUIContent("Node Graph Editor", NodeEditorResources.graph);
            w.wantsMouseMove = true;
            w.Show();
            return w;
        }

        public void Save() {
            if (AssetDatabase.Contains(graph)) {
                EditorUtility.SetDirty(graph);
                if (NodeEditorPreferences.GetSettings().autoSave) AssetDatabase.SaveAssets();
            } else SaveAs();
        }

        public void SaveAs() {
            string path = EditorUtility.SaveFilePanelInProject("Save NodeGraph", "NewNodeGraph", "asset", "");
            if (string.IsNullOrEmpty(path)) return;
            else {
                XMonoNode.NodeGraph existingGraph = AssetDatabase.LoadAssetAtPath<XMonoNode.NodeGraph>(path);
                if (existingGraph != null) AssetDatabase.DeleteAsset(path);
                AssetDatabase.CreateAsset(graph, path);
                EditorUtility.SetDirty(graph);
                if (NodeEditorPreferences.GetSettings().autoSave) AssetDatabase.SaveAssets();
            }
        }

        private void DraggableWindow(int windowID) {
            GUI.DragWindow();
        }

        public Vector2 WindowToGridPosition(Vector2 windowPosition) {
            return (windowPosition - (position.size * 0.5f) - (panOffset / zoom)) * zoom;
        }

        public Vector2 GridToWindowPosition(Vector2 gridPosition) {
            return (position.size * 0.5f) + (panOffset / zoom) + (gridPosition / zoom);
        }

        public Rect GridToWindowRectNoClipped(Rect gridRect) {
            gridRect.position = GridToWindowPositionNoClipped(gridRect.position);
            return gridRect;
        }

        public Rect GridToWindowRect(Rect gridRect) {
            gridRect.position = GridToWindowPosition(gridRect.position);
            gridRect.size /= zoom;
            return gridRect;
        }

        public Vector2 GridToWindowPositionNoClipped(Vector2 gridPosition) {
            Vector2 center = position.size * 0.5f;
            // UI Sharpness complete fix - Round final offset not panOffset
            float xOffset = Mathf.Round(center.x * zoom + (panOffset.x + gridPosition.x));
            float yOffset = Mathf.Round(center.y * zoom + (panOffset.y + gridPosition.y));
            return new Vector2(xOffset, yOffset);
        }

        public void SelectNode(XMonoNode.INode node, bool add) {
            if (add) {
                List<Object> selection = new List<Object>(Selection.objects);
                selection.Add(node as UnityEngine.Object);
                Selection.objects = selection.ToArray();
            } else Selection.objects = new Object[] { node as UnityEngine.Object };
        }

        public void DeselectNode(XMonoNode.INode node) {
            List<Object> selection = new List<Object>(Selection.objects);
            selection.Remove(node as UnityEngine.Object);
            Selection.objects = selection.ToArray();
        }

        [OnOpenAsset(0)]
        public static bool OnOpen(int instanceID, int line) {
            XMonoNode.NodeGraph nodeGraph = EditorUtility.InstanceIDToObject(instanceID) as XMonoNode.NodeGraph;
            if (nodeGraph != null) {
                Open(nodeGraph);
                return true;
            }
            return false;
        }

        /// <summary>Open the provided graph in the NodeEditor</summary>
        public static NodeEditorWindow Open(XMonoNode.INodeGraph graph) {
            if ((graph as UnityEngine.Object) == null) return null;

            NodeEditorWindow w = GetWindow(typeof(NodeEditorWindow), false, "Node Editor", true) as NodeEditorWindow;
            w.wantsMouseMove = true;
            w.graph = graph as UnityEngine.Object;
            return w;
        }

        /// <summary> Repaint all open NodeEditorWindows. </summary>
        public static void RepaintAll() {
            NodeEditorWindow[] windows = Resources.FindObjectsOfTypeAll<NodeEditorWindow>();
            for (int i = 0; i < windows.Length; i++) {
                windows[i].Repaint();
            }
        }
    }
}
