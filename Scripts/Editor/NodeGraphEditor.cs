using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace XMonoNodeEditor {
    /// <summary> Base class to derive custom Node Graph editors from. Use this to override how graphs are drawn in the editor. </summary>
    [CustomNodeGraphEditor(typeof(XMonoNode.INodeGraph))]
    public class NodeGraphEditor : XMonoNodeEditor.Internal.NodeEditorBase<NodeGraphEditor, NodeGraphEditor.CustomNodeGraphEditorAttribute, XMonoNode.INodeGraph> {
        [Obsolete("Use window.position instead")]
        public Rect position { get { return window.position; } set { window.position = value; } }
        /// <summary> Are we currently renaming a node? </summary>
        protected bool isRenaming;

        public UnityEngine.Object target
        {
            get
            {
                return Target as UnityEngine.Object;
            }
        }

        public virtual void OnGUI() { }

        /// <summary> We can draw </summary>
        public virtual void OnToolBarGUI()
        {

        }

        /// <summary> Called when opened by NodeEditorWindow </summary>
        public virtual void OnOpen()
        {
            window.titleContent = new GUIContent("Node Graph", NodeEditorResources.graph);
        }

        /// <summary> Called when NodeEditorWindow gains focus </summary>
        public virtual void OnWindowFocus() { }

        /// <summary> Called when NodeEditorWindow loses focus </summary>
        public virtual void OnWindowFocusLost() { }

        public virtual Texture2D GetGridTexture() {
            return NodeEditorPreferences.GetSettings().gridTexture;
        }

        public virtual Texture2D GetSecondaryGridTexture() {
            return NodeEditorPreferences.GetSettings().crossTexture;
        }

        /// <summary> Return default settings for this graph type. This is the settings the user will load if no previous settings have been saved. </summary>
        public virtual NodeEditorPreferences.Settings GetDefaultPreferences() {
            return new NodeEditorPreferences.Settings();
        }

        /// <summary> Returns context node menu path. Null or empty strings for hidden nodes. </summary>
        public virtual string GetNodeMenuName(Type type) {
            //Check if type has the CreateNodeMenuAttribute
            XMonoNode.CreateNodeMenuAttribute attrib;
            if (NodeEditorUtilities.GetAttrib(type, out attrib)) // Return custom path
                return attrib.menuName;
            else // Return generated path
                return NodeEditorUtilities.NodeDefaultPath(type);
        }

        /// <summary> The order by which the menu items are displayed. </summary>
        public virtual int GetNodeMenuOrder(Type type) {
            //Check if type has the CreateNodeMenuAttribute
            XMonoNode.CreateNodeMenuAttribute attrib;
            if (NodeEditorUtilities.GetAttrib(type, out attrib)) // Return custom path
                return attrib.order;
            else
                return 0;
        }

        /// <summary>
        /// Add custom context menu items for hovered port
        /// </summary>
        public virtual void addCustomContextMenuItemsForPort(GenericMenu contextMenu, XMonoNode.NodePort hoveredPort)
        {
            
        }

        /// <summary>
        /// Add items for the context menu when right-clicking this node.
        /// Override to add custom menu items.
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="compatibleType">Use it to filter only nodes with ports value type, compatible with this type</param>
        /// <param name="direction">Direction of the compatiblity</param>
        public virtual void AddContextMenuItems(GenericMenu menu, Type compatibleType = null, XMonoNode.NodePort.IO direction = XMonoNode.NodePort.IO.Input) {
            Vector2 pos = NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition);

            Type[] nodeTypes;

            if (compatibleType != null && NodeEditorPreferences.GetSettings().createFilter) {
                nodeTypes = NodeEditorUtilities.GetCompatibleNodesTypes(NodeEditorReflection.nodeTypes, compatibleType, direction).OrderBy(GetNodeMenuOrder).ToArray();
            } else {
                nodeTypes = NodeEditorReflection.nodeTypes.OrderBy(GetNodeMenuOrder).ToArray();
            }

            Type nodeBaseType = Target.getNodeType();

            for (int i = 0; i < nodeTypes.Length; i++) {
                Type type = nodeTypes[i];

                if (!type.IsSubclassOf(nodeBaseType))
                {
                    continue;
                }

                //Get node context menu path
                string path = "Create/" + GetNodeMenuName(type);
                if (string.IsNullOrEmpty(path)) continue;

                // Check if user is allowed to add more of given node type
                XMonoNode.DisallowMultipleNodesAttribute disallowAttrib;
                bool disallowed = false;
                if (NodeEditorUtilities.GetAttrib(type, out disallowAttrib)) {
                    int typeCount = Target.GetNodes().Count(x => x.GetType() == type);
                    if (typeCount >= disallowAttrib.max) disallowed = true;
                }

                // Add node entry to context menu
                if (disallowed) menu.AddItem(new GUIContent(path), false, null);
                else menu.AddItem(new GUIContent(path), false, () => {
                    XMonoNode.INode node = CreateNode(type, pos);
                    NodeEditorWindow.current.AutoConnect(node);
                    Selection.objects = new UnityEngine.Object[1] { node as UnityEngine.Object };
                });
            }
            menu.AddSeparator("");
            if (NodeEditorWindow.copyBuffer != null && NodeEditorWindow.copyBuffer.Length > 0) menu.AddItem(new GUIContent("Paste"), false, () => NodeEditorWindow.current.PasteNodes(pos));
            else menu.AddDisabledItem(new GUIContent("Paste"));
            menu.AddItem(new GUIContent("Preferences"), false, () => NodeEditorReflection.OpenPreferences());
            menu.AddCustomContextMenuItems(target);
        }

        /// <summary> Returned gradient is used to color noodles </summary>
        /// <param name="output"> The output this noodle comes from. Never null. </param>
        /// <param name="input"> The output this noodle comes from. Can be null if we are dragging the noodle. </param>
        public virtual Gradient GetNoodleGradient(XMonoNode.NodePort output, XMonoNode.NodePort input) {
            Gradient grad = new Gradient();

            // If dragging the noodle, draw solid, slightly transparent
            if (input == null) {
                Color a = GetTypeColor(output.ValueType);
                grad.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(a, 0f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(0.6f, 0f) }
                );
            }
            // If normal, draw gradient fading from one input color to the other
            else {
                Color a = GetTypeColor(output.ValueType);
                Color b = GetTypeColor(input.ValueType);
                // If any port is hovered, tint white
                if (window.hoveredPort == output || window.hoveredPort == input) {
                    a = Color.Lerp(a, Color.white, 0.8f);
                    b = Color.Lerp(b, Color.white, 0.8f);
                }
                grad.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(a, 0f), new GradientColorKey(b, 1f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
                );
            }
            return grad;
        }

        /// <summary> Returned float is used for noodle thickness </summary>
        /// <param name="output"> The output this noodle comes from. Never null. </param>
        /// <param name="input"> The output this noodle comes from. Can be null if we are dragging the noodle. </param>
        public virtual float GetNoodleThickness(XMonoNode.NodePort output, XMonoNode.NodePort input) {
            return NodeEditorPreferences.GetSettings().noodleThickness;
        }

        public virtual NoodlePath GetNoodlePath(XMonoNode.NodePort output, XMonoNode.NodePort input) {
            return NodeEditorPreferences.GetSettings().noodlePath;
        }

        public virtual NoodleStroke GetNoodleStroke(XMonoNode.NodePort output, XMonoNode.NodePort input) {
            return NodeEditorPreferences.GetSettings().noodleStroke;
        }

        /// <summary> Returned color is used to color ports </summary>
        public virtual Color GetPortColor(XMonoNode.NodePort port) {
            return GetTypeColor(port.ValueType);
        }

        /// <summary>
        /// The returned Style is used to configure the paddings and icon texture of the ports.
        /// Use these properties to customize your port style.
        ///
        /// The properties used is:
        /// <see cref="GUIStyle.padding"/>[Left and Right], <see cref="GUIStyle.normal"/> [Background] = border texture,
        /// and <seealso cref="GUIStyle.active"/> [Background] = dot texture;
        /// </summary>
        /// <param name="port">the owner of the style</param>
        /// <returns></returns>
        public virtual GUIStyle GetPortStyle(XMonoNode.NodePort port) {
            if (port.direction == XMonoNode.NodePort.IO.Input)
                return NodeEditorResources.styles.inputPort;

            return NodeEditorResources.styles.outputPort;
        }

        /// <summary> The returned color is used to color the background of the door.
        /// Usually used for outer edge effect </summary>
        public virtual Color GetPortBackgroundColor(XMonoNode.NodePort port) {
            return Color.gray;
        }

        /// <summary> Returns generated color for a type. This color is editable in preferences </summary>
        public virtual Color GetTypeColor(Type type)
        {
            return NodeEditorPreferences.GetTypeColor(type);
        }

        /// <summary> Override to display custom tooltips </summary>
        public virtual string GetPortTooltip(XMonoNode.NodePort port)
        {
            Type portType = port.ValueType;
            string tooltip = XMonoNode.NodeUtilities.PrettyName(portType);
            if (port.IsOutput)
            {
                object obj = port.node.GetValue(port);
                tooltip += " = " + (obj != null ? obj.ToString() : "null");
            }
            return tooltip;
        }

        /// <summary> Deal with objects dropped into the graph through DragAndDrop </summary>
        public virtual void OnDropObjects(UnityEngine.Object[] objects)
        {
            //if (GetType() != typeof(NodeGraphEditor)) Debug.Log("No OnDropObjects override defined for " + GetType());
        }

        /// <summary> Create a node and save it in the graph asset </summary>
        public virtual XMonoNode.INode CreateNode(Type type, Vector2 position)
        {
            Undo.RecordObject(target, "Create Node");
            XMonoNode.INode node = Target.AddNode(type);
            Undo.RegisterCreatedObjectUndo(node as UnityEngine.Object, "Create Node");
            node.Position = position;
            if (node.Name == null || node.Name.Trim() == "") node.Name = NodeEditorUtilities.NodeDefaultName(type);
         
            if (EditorUtility.IsPersistent(target) && !AssetDatabase.Contains(node as UnityEngine.Object))
            {
                if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(target))) AssetDatabase.AddObjectToAsset(node as UnityEngine.Object, target);
            }
            
            if (NodeEditorPreferences.GetSettings().autoSave) AssetDatabase.SaveAssets();
            NodeEditorWindow.RepaintAll();
            return node;
        }

        /// <summary> Creates a copy of the original node in the graph </summary>
        public virtual XMonoNode.INode CopyNode(XMonoNode.INode original)
        {
            Undo.RecordObject(target, "Duplicate Node");
            XMonoNode.INode node = Target.CopyNode(original);
            Undo.RegisterCreatedObjectUndo(node as UnityEngine.Object, "Duplicate Node");
            node.Name = original.Name;
            if (EditorUtility.IsPersistent(target) && !AssetDatabase.Contains(node as UnityEngine.Object))
            {
                AssetDatabase.AddObjectToAsset(node as UnityEngine.Object, target);
            }

            if (NodeEditorPreferences.GetSettings().autoSave) AssetDatabase.SaveAssets();
            return node;
        }

        /// <summary> Return false for nodes that can't be removed </summary>
        public virtual bool CanRemove(XMonoNode.INode node) {
            // Check graph attributes to see if this node is required
            Type graphType = target.GetType();
            XMonoNode.RequireNodeAttribute[] attribs = Array.ConvertAll(
                graphType.GetCustomAttributes(typeof(XMonoNode.RequireNodeAttribute), false), x => x as XMonoNode.RequireNodeAttribute);
            if (attribs.Any(x => x.Requires(node.GetType()))) {
                if (Target.GetNodes().Count(x => x.GetType() == node.GetType()) <= 1) {
                    return false;
                }
            }
            return true;
        }

        /// <summary> Safely remove a node and all its connections. </summary>
        public virtual void RemoveNode(XMonoNode.INode node) {
            if (!CanRemove(node)) return;
           
            // Remove the node
            Undo.RecordObject(node as UnityEngine.Object, "Delete Node");
            Undo.RecordObject(target, "Delete Node");
            foreach (var port in node.Ports)
                foreach (var conn in port.GetConnections())
                    Undo.RecordObject(conn.node as UnityEngine.Object, "Delete Node");
   
       
            Target.RemoveNode(node);

            if (Application.isEditor)
            {
                if (node as UnityEngine.Object != null)
                    Undo.DestroyObjectImmediate(node as UnityEngine.Object);
                if (NodeEditorPreferences.GetSettings().autoSave)
                    AssetDatabase.SaveAssets();
            }

            
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class CustomNodeGraphEditorAttribute : Attribute,
        XMonoNodeEditor.Internal.NodeEditorBase<NodeGraphEditor, NodeGraphEditor.CustomNodeGraphEditorAttribute, XMonoNode.INodeGraph>.INodeEditorAttrib {
            private Type inspectedType;
            public string editorPrefsKey;
            /// <summary> Tells a NodeGraphEditor which Graph type it is an editor for </summary>
            /// <param name="inspectedType">Type that this editor can edit</param>
            /// <param name="editorPrefsKey">Define unique key for unique layout settings instance</param>
            public CustomNodeGraphEditorAttribute(Type inspectedType, string editorPrefsKey = "xNode.Settings") {
                this.inspectedType = inspectedType;
                this.editorPrefsKey = editorPrefsKey;
            }

            public Type GetInspectedType() {
                return inspectedType;
            }
        }
    }
}