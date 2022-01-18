using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace XMonoNodeEditor {
    /// <summary> xNode-specific version of <see cref="EditorGUILayout"/> </summary>
    public static class NodeEditorGUILayout {

        private static readonly Dictionary<UnityEngine.Object, Dictionary<string, ReorderableList>> reorderableListCache = new Dictionary<UnityEngine.Object, Dictionary<string, ReorderableList>>();
        private static int reorderableListIndex = -1;
        public static bool hasHiddenProperty = false;

        /// <summary> Make a field for a serialized property. Automatically displays relevant node port. </summary>
        public static void PropertyField(SerializedProperty property, bool includeChildren = true, params GUILayoutOption[] options)
        {
            PropertyField(property, (GUIContent)null, includeChildren, options);
        }

        /// <summary> Make a field for a serialized property. Automatically displays relevant node port. </summary>
        public static void PropertyField(SerializedProperty property, GUIContent label, bool includeChildren = true, params GUILayoutOption[] options)
        {
            if (property == null)
                throw new NullReferenceException();

            XMonoNode.INode node = property.serializedObject.targetObject as XMonoNode.INode;

            if (NodeEditorUtilities.GetCachedAttrib(node.GetType(), property.name, out XMonoNode.HideInNodeInspectorAttribute hideAttribute))
            {
                return;
            }

            XMonoNode.NodePort port = node.GetPort(property.name);

            PropertyField(property, label, node, port, includeChildren);
        }

        /// <summary> Make a field for a serialized property. Manual node port override. </summary>
        public static void PropertyField(SerializedProperty property, XMonoNode.INode node, XMonoNode.NodePort port, bool includeChildren = true, params GUILayoutOption[] options)
        {
            PropertyField(property, null, node, port, includeChildren, options);
        }

        /// <summary> Make a field for a serialized property. Manual node port override. </summary>
        public static void PropertyField(SerializedProperty property, GUIContent label, XMonoNode.INode node, XMonoNode.NodePort port, bool includeChildren = true, params GUILayoutOption[] options)
        {
            if (property == null)
                throw new NullReferenceException();

            NodeEditorUtilities.GetCachedAttrib(node.GetType(), property.name, out XMonoNode.HidingAttribute hidding);
            

            NodeEditorUtilities.GetCachedAttrib(node.GetType(), property.name, out XMonoNode.NodeInspectorButtonAttribute buttonAttribute);

            NodeEditorUtilities.GetCachedAttrib(node.GetType(), property.name, out XMonoNode.HideLabelAttribute hideLabelAttribute);

            if (hideLabelAttribute != null)
            {
                label = new GUIContent();
            }


            // If property is not a port, display a regular property field
            if (port == null)
            {
                if (node.ShowState == XMonoNode.INode.ShowAttribState.Minimize ||
                    (hidding != null && node.ShowState == XMonoNode.INode.ShowAttribState.ShowBase))
                {// Пропускаем скрытые свойства
                    NodeEditorGUILayout.hasHiddenProperty = true;
                    return;
                }

                if (NodeEditorPreferences.GetSettings().showPortButton(buttonAttribute))
                {
                    GUILayout.Button(label != null ? label : new GUIContent(property.displayName));
                }
                else
                {
                    EditorGUILayout.PropertyField(property, label, includeChildren, GUILayout.MinWidth(30));
                    
                }
            }
            else
            {
                if (port.ConnectionCount == 0 && node.ShowState == XMonoNode.INode.ShowAttribState.Minimize)
                {// Нода скрыта - пропускаем все несоединенные порты
                    NodeEditorGUILayout.hasHiddenProperty = true;
                    return; 
                }
                if (port.ConnectionCount == 0 && hidding != null && node.ShowState == XMonoNode.INode.ShowAttribState.ShowBase)
                {// Нода частично скрыта - пропускаем скрытые несоединенные порты
                    NodeEditorGUILayout.hasHiddenProperty = true;
                    return;
                }

                Rect rect = new Rect();

                List<PropertyAttribute> propertyAttributes = NodeEditorUtilities.GetCachedPropertyAttribs(port.node.GetType(), property.name);

                // If property is an input, display a regular property field and put a port handle on the left side
                if (port.direction == XMonoNode.NodePort.IO.Input)
                {
                    // Get data from [Input] attribute
                    XMonoNode.ShowBackingValue showBacking = XMonoNode.ShowBackingValue.Unconnected;
                    XMonoNode.TypeConstraint typeConstraint = XMonoNode.TypeConstraint.Inherited;
                    XMonoNode.InputAttribute inputAttribute;
                    bool dynamicPortList = false;
                    if (NodeEditorUtilities.GetCachedAttrib(port.node.GetType(), property.name, out inputAttribute))
                    {
                        dynamicPortList = inputAttribute.dynamicPortList;
                        showBacking = inputAttribute.backingValue;
                        typeConstraint = inputAttribute.typeConstraint;
                        if (hideLabelAttribute == null)
                        {
                            label = portGuiContent(port);
                        }
                    }

                    bool usePropertyAttributes = dynamicPortList ||
                        showBacking == XMonoNode.ShowBackingValue.Never ||
                        (showBacking == XMonoNode.ShowBackingValue.Unconnected && port.IsConnected);

                    float spacePadding = 0;
                    string tooltip = null;

                    foreach (var attr in propertyAttributes)
                    {
                        if (attr is SpaceAttribute)
                        {
                            if (usePropertyAttributes)
                                GUILayout.Space((attr as SpaceAttribute).height);
                            else
                                spacePadding += (attr as SpaceAttribute).height;
                        }
                        else if (attr is HeaderAttribute)
                        {
                            if (usePropertyAttributes)
                            {
                                //GUI Values are from https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/ScriptAttributeGUI/Implementations/DecoratorDrawers.cs
                                Rect position = GUILayoutUtility.GetRect(0, (EditorGUIUtility.singleLineHeight * 1.5f) - EditorGUIUtility.standardVerticalSpacing); //Layout adds standardVerticalSpacing after rect so we subtract it.
                                position.yMin += EditorGUIUtility.singleLineHeight * 0.5f;
                                position = EditorGUI.IndentedRect(position);
                                GUI.Label(position, (attr as HeaderAttribute).header, EditorStyles.boldLabel);
                            }
                            else
                                spacePadding += EditorGUIUtility.singleLineHeight * 1.5f;
                        }
                        else if (attr is TooltipAttribute)
                        {
                            tooltip = (attr as TooltipAttribute).tooltip;
                        }
                    }

                    if (dynamicPortList)
                    {
                        Type type = GetType(property);
                        XMonoNode.ConnectionType connectionType = inputAttribute != null ? inputAttribute.connectionType : XMonoNode.ConnectionType.Multiple;
                        DynamicPortList(property.name, type, property.serializedObject, port.direction, connectionType, typeConstraint, showBacking, buttonAttribute);
                        return;
                    }

                    if (NodeEditorPreferences.GetSettings().showPortButton(buttonAttribute))
                    {
                        NodeEditorUtilities.AddPortButtonPressed(port, GUILayout.Button(label));
                    }
                    else
                    {
                        switch (showBacking)
                        {
                            case XMonoNode.ShowBackingValue.Unconnected:
                                // Display a label if port is connected
                                if (port.IsConnected)
                                    GUILayout.Label(label != null && label.text != "" ? label : new GUIContent(property.displayName, tooltip));
                                // Display an editable property field if port is not connected
                                else
                                    PropertyField(property, label, includeChildren);
                                break;
                            case XMonoNode.ShowBackingValue.Never:
                                // Display a label
                                GUILayout.Label(label != null && label.text != "" ? label : new GUIContent(property.displayName, tooltip));
                                break;
                            case XMonoNode.ShowBackingValue.Always:
                                // Display an editable property field
                                PropertyField(property, label, includeChildren);
                                break;
                        }
                    }

                    rect = GUILayoutUtility.GetLastRect();
                    float paddingLeft = NodeEditorWindow.current.graphEditor.GetPortStyle(port).padding.left;
                    rect.position = rect.position - new Vector2(16 + paddingLeft, -spacePadding);
                    // If property is an output, display a text label and put a port handle on the right side
                }
                else if (port.direction == XMonoNode.NodePort.IO.Output)
                {
                    // Get data from [Output] attribute
                    XMonoNode.ShowBackingValue showBacking = XMonoNode.ShowBackingValue.Unconnected;
                    XMonoNode.TypeConstraint typeConstraint = XMonoNode.TypeConstraint.Inherited;
                    XMonoNode.OutputAttribute outputAttribute;
                    bool dynamicPortList = false;
                    if (NodeEditorUtilities.GetCachedAttrib(port.node.GetType(), property.name, out outputAttribute))
                    {
                        dynamicPortList = outputAttribute.dynamicPortList;
                        showBacking = outputAttribute.backingValue;
                        typeConstraint = outputAttribute.typeConstraint;
                        if (hideLabelAttribute == null)
                        {
                            label = portGuiContent(port);
                        }
                    }

                    bool usePropertyAttributes = dynamicPortList ||
                        showBacking == XMonoNode.ShowBackingValue.Never ||
                        (showBacking == XMonoNode.ShowBackingValue.Unconnected && port.IsConnected);

                    float spacePadding = 0;
                    string tooltip = null;
                    foreach (var attr in propertyAttributes)
                    {
                        if (attr is SpaceAttribute)
                        {
                            if (usePropertyAttributes)
                                GUILayout.Space((attr as SpaceAttribute).height);
                            else
                                spacePadding += (attr as SpaceAttribute).height;
                        }
                        else if (attr is HeaderAttribute)
                        {
                            if (usePropertyAttributes)
                            {
                                //GUI Values are from https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/ScriptAttributeGUI/Implementations/DecoratorDrawers.cs
                                Rect position = GUILayoutUtility.GetRect(0, (EditorGUIUtility.singleLineHeight * 1.5f) - EditorGUIUtility.standardVerticalSpacing); //Layout adds standardVerticalSpacing after rect so we subtract it.
                                position.yMin += EditorGUIUtility.singleLineHeight * 0.5f;
                                position = EditorGUI.IndentedRect(position);
                                GUI.Label(position, (attr as HeaderAttribute).header, EditorStyles.boldLabel);
                            }
                            else
                                spacePadding += EditorGUIUtility.singleLineHeight * 1.5f;
                        }
                        else if (attr is TooltipAttribute)
                        {
                            tooltip = (attr as TooltipAttribute).tooltip;
                        }
                    }

                    if (dynamicPortList)
                    {
                        Type type = GetType(property);
                        XMonoNode.ConnectionType connectionType = outputAttribute != null ? outputAttribute.connectionType : XMonoNode.ConnectionType.Multiple;
                        DynamicPortList(property.name, type, property.serializedObject, port.direction, connectionType, typeConstraint, showBacking, buttonAttribute);
                        return;
                    }
                    if (NodeEditorPreferences.GetSettings().showPortButton(buttonAttribute))
                    {
                        NodeEditorUtilities.AddPortButtonPressed(port, GUILayout.Button(label));
                    }
                    else
                    { 
                        switch (showBacking)
                        {
                            case XMonoNode.ShowBackingValue.Unconnected:
                                if (port.IsConnected)
                                {
                                    GUILayout.Label(label != null && label.text != "" ? label : new GUIContent(property.displayName, tooltip), NodeEditorResources.OutputPort, GUILayout.MinWidth(30));
                                }
                                else
                                {
                                    PropertyField(property, label, includeChildren);
                                }
                                break;
                            case XMonoNode.ShowBackingValue.Never:
                                GUILayout.Label(label != null && label.text != "" ? label : new GUIContent(property.displayName, tooltip), NodeEditorResources.OutputPort, GUILayout.MinWidth(30));
                                break;
                            case XMonoNode.ShowBackingValue.Always:
                                PropertyField(property, label, includeChildren);
                                break;
                        }
                    }

                    rect = GUILayoutUtility.GetLastRect();
                    rect.width += NodeEditorWindow.current.graphEditor.GetPortStyle(port).padding.right;
                    rect.position = rect.position + new Vector2(rect.width, spacePadding);
                }

                rect.size = new Vector2(16, 16);

                Color backgroundColor = NodeEditorWindow.current.graphEditor.GetPortBackgroundColor(port);
                Color col = NodeEditorWindow.current.graphEditor.GetPortColor(port);
                GUIStyle portStyle = NodeEditorWindow.current.graphEditor.GetPortStyle(port);
                DrawPortHandle(rect, backgroundColor, col, portStyle.normal.background, portStyle.active.background);

                // Register the handle position
                Vector2 portPos = rect.center;
                NodeEditor.portPositions[port] = portPos;
            }
        }

        private static void PropertyField(SerializedProperty property, GUIContent label, bool includeChildren)
        {
            if (property.propertyType == SerializedPropertyType.Enum &&
                NodeEditorWindow.current != null &&
                NodeEditorWindow.current.zoom > 1.3f)
            {
                EditorGUILayout.LabelField(label, new GUIContent("zoom out..."), GUILayout.MinWidth(30));
            }
            else
            {
                EditorGUILayout.PropertyField(property, label, includeChildren, GUILayout.MinWidth(30));
            }
        }

        private static System.Type GetType(SerializedProperty property) {
            System.Type parentType = property.serializedObject.targetObject.GetType();
            System.Reflection.FieldInfo fi = parentType.GetFieldInfo(property.name);
            return fi.FieldType;
        }

        /// <summary> Make a simple port field. </summary>
        public static void PortField(XMonoNode.NodePort port, params GUILayoutOption[] options) {
            PortField(null, port, options);
        }

        public static GUIContent portGuiContent(XMonoNode.NodePort port)
        {
            return new GUIContent(string.IsNullOrEmpty(port.label) ? ObjectNames.NicifyVariableName(port.fieldName) : port.label);
        }

        /// <summary> Make a simple port field. </summary>
        public static void PortField(GUIContent label, XMonoNode.NodePort port, params GUILayoutOption[] options) {
            if (port == null) return;
            if (options == null) options = new GUILayoutOption[] { GUILayout.MinWidth(30) };
            Vector2 position = Vector3.zero;
            GUIContent content = label != null ? label : portGuiContent(port);

            // If property is an input, display a regular property field and put a port handle on the left side
            if (port.direction == XMonoNode.NodePort.IO.Input)
            {
                // Display a label
                GUILayout.Label(content, options);

                Rect rect = GUILayoutUtility.GetLastRect();
                float paddingLeft = NodeEditorWindow.current.graphEditor.GetPortStyle(port).padding.left;
                position = rect.position - new Vector2(16 + paddingLeft, 0);
            }
            // If property is an output, display a text label and put a port handle on the right side
            else if (port.direction == XMonoNode.NodePort.IO.Output) {
                // Display a label
                GUILayout.Label(content, NodeEditorResources.OutputPort, options);

                Rect rect = GUILayoutUtility.GetLastRect();
                rect.width += NodeEditorWindow.current.graphEditor.GetPortStyle(port).padding.right;
                position = rect.position + new Vector2(rect.width, 0);
            }
            PortField(position, port);
        }

        /// <summary> Make a simple port field. </summary>
        public static void PortField(Vector2 position, XMonoNode.NodePort port) {
            if (port == null) return;

            Rect rect = new Rect(position, new Vector2(16, 16));

            Color backgroundColor = NodeEditorWindow.current.graphEditor.GetPortBackgroundColor(port);
            Color col = NodeEditorWindow.current.graphEditor.GetPortColor(port);
            GUIStyle portStyle = NodeEditorWindow.current.graphEditor.GetPortStyle(port);

            DrawPortHandle(rect, backgroundColor, col, portStyle.normal.background, portStyle.active.background);

            // Register the handle position
            Vector2 portPos = rect.center;
            NodeEditor.portPositions[port] = portPos;
        }

        /// <summary> Add a port field to previous layout element. </summary>
        public static void AddPortField(XMonoNode.NodePort port) {
            if (port == null) return;
            Rect rect = new Rect();

            // If property is an input, display a regular property field and put a port handle on the left side
            if (port.direction == XMonoNode.NodePort.IO.Input) {
                rect = GUILayoutUtility.GetLastRect();
                float paddingLeft = NodeEditorWindow.current.graphEditor.GetPortStyle(port).padding.left;
                rect.position = rect.position - new Vector2(16 + paddingLeft, 0);
                // If property is an output, display a text label and put a port handle on the right side
            } else if (port.direction == XMonoNode.NodePort.IO.Output) {
                rect = GUILayoutUtility.GetLastRect();
                rect.width += NodeEditorWindow.current.graphEditor.GetPortStyle(port).padding.right;
                rect.position = rect.position + new Vector2(rect.width, 0);
            }

            rect.size = new Vector2(16, 16);

            Color backgroundColor = NodeEditorWindow.current.graphEditor.GetPortBackgroundColor(port);
            Color col = NodeEditorWindow.current.graphEditor.GetPortColor(port);
            GUIStyle portStyle = NodeEditorWindow.current.graphEditor.GetPortStyle(port);

            DrawPortHandle(rect, backgroundColor, col, portStyle.normal.background, portStyle.active.background);

            // Register the handle position
            Vector2 portPos = rect.center;
            NodeEditor.portPositions[port] = portPos;
        }

        /// <summary> Draws an input and an output port on the same line </summary>
        public static void PortPair(XMonoNode.NodePort input, XMonoNode.NodePort output) {
            GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(input, GUILayout.MinWidth(0));
            NodeEditorGUILayout.PortField(output, GUILayout.MinWidth(0));
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draw the port
        /// </summary>
        /// <param name="rect">position and size</param>
        /// <param name="backgroundColor">color for background texture of the port. Normaly used to Border</param>
        /// <param name="typeColor"></param>
        /// <param name="border">texture for border of the dot port</param>
        /// <param name="dot">texture for the dot port</param>
        public static void DrawPortHandle(Rect rect, Color backgroundColor, Color typeColor, Texture2D border, Texture2D dot) {
            Color col = GUI.color;
            GUI.color = backgroundColor;
            GUI.DrawTexture(rect, border);
            GUI.color = typeColor;
            GUI.DrawTexture(rect, dot);
            GUI.color = col;
        }


        #region Obsolete
        [Obsolete("Use IsDynamicPortListPort instead")]
        public static bool IsInstancePortListPort(XMonoNode.NodePort port) {
            return IsDynamicPortListPort(port);
        }

        [Obsolete("Use DynamicPortList instead")]
        public static void InstancePortList(string fieldName, Type type, SerializedObject serializedObject, XMonoNode.NodePort.IO io, XMonoNode.ConnectionType connectionType = XMonoNode.ConnectionType.Multiple, XMonoNode.TypeConstraint typeConstraint = XMonoNode.TypeConstraint.None, Action<ReorderableList> onCreation = null) {
            DynamicPortList(fieldName, type, serializedObject, io, connectionType, typeConstraint, XMonoNode.ShowBackingValue.Unconnected, null, onCreation);
        }
        #endregion

        /// <summary> Is this port part of a DynamicPortList? </summary>
        public static bool IsDynamicPortListPort(XMonoNode.NodePort port) {
            string[] parts = port.fieldName.Split(' ');
            if (parts.Length != 2) return false;
            Dictionary<string, ReorderableList> cache;
            if (reorderableListCache.TryGetValue(port.node as UnityEngine.Object, out cache)) {
                ReorderableList list;
                if (cache.TryGetValue(parts[0], out list)) return true;
            }
            return false;
        }

        /// <summary> Draw an editable list of dynamic ports. Port names are named as "[fieldName] [index]" </summary>
        /// <param name="fieldName">Supply a list for editable values</param>
        /// <param name="type">Value type of added dynamic ports</param>
        /// <param name="serializedObject">The serializedObject of the node</param>
        /// <param name="connectionType">Connection type of added dynamic ports</param>
        /// <param name="onCreation">Called on the list on creation. Use this if you want to customize the created ReorderableList</param>
        public static void DynamicPortList(
            string fieldName,
            Type type, SerializedObject serializedObject,
            XMonoNode.NodePort.IO io,
            XMonoNode.ConnectionType connectionType = XMonoNode.ConnectionType.Multiple,
            XMonoNode.TypeConstraint typeConstraint = XMonoNode.TypeConstraint.None,
            XMonoNode.ShowBackingValue showBacking = XMonoNode.ShowBackingValue.Unconnected,
            XMonoNode.NodeInspectorButtonAttribute buttonAttribute = null,
            Action<ReorderableList> onCreation = null)
        {
            XMonoNode.INode node = serializedObject.targetObject as XMonoNode.INode;

            var indexedPorts = node.DynamicPorts.Select(x => {
                string[] split = x.fieldName.Split(' ');
                if (split != null && split.Length == 2 && split[0] == fieldName) {
                    int i = -1;
                    if (int.TryParse(split[1], out i)) {
                        return new { index = i, port = x };
                    }
                }
                return new { index = -1, port = (XMonoNode.NodePort)null };
            }).Where(x => x.port != null);
            List<XMonoNode.NodePort> dynamicPorts = indexedPorts.OrderBy(x => x.index).Select(x => x.port).ToList();

            node.UpdatePorts();

            ReorderableList list = null;
            Dictionary<string, ReorderableList> rlc;
            if (reorderableListCache.TryGetValue(serializedObject.targetObject, out rlc)) {
                if (!rlc.TryGetValue(fieldName, out list)) list = null;
            }
            // If a ReorderableList isn't cached for this array, do so.
            if (list == null)
            {
                SerializedProperty arrayData = serializedObject.FindProperty(fieldName);
                list = CreateReorderableList(fieldName, dynamicPorts, arrayData, type, serializedObject, io, connectionType, typeConstraint, showBacking, buttonAttribute, onCreation);
                if (reorderableListCache.TryGetValue(serializedObject.targetObject, out rlc))
                {
                    try
                    {
                        rlc.Add(fieldName, list);
                    }
                    catch (ArgumentException )
                    {
                        // nothing
                    }
                }
                else
                    reorderableListCache.Add(serializedObject.targetObject, new Dictionary<string, ReorderableList>() { { fieldName, list } });
            }
            list.list = dynamicPorts;
            list.DoLayoutList();

        }

        private static ReorderableList CreateReorderableList(
            string fieldName,
            List<XMonoNode.NodePort> dynamicPorts,
            SerializedProperty arrayData,
            Type type,
            SerializedObject serializedObject,
            XMonoNode.NodePort.IO io,
            XMonoNode.ConnectionType connectionType,
            XMonoNode.TypeConstraint typeConstraint,
            XMonoNode.ShowBackingValue showBacking,
            XMonoNode.NodeInspectorButtonAttribute buttonAttribute,
            Action<ReorderableList> onCreation)
        {
            bool hasArrayData = arrayData != null && arrayData.isArray;
            XMonoNode.INode node = serializedObject.targetObject as XMonoNode.INode;
            ReorderableList list = new ReorderableList(dynamicPorts, null, true, true, true, true);
            string label = arrayData != null ? arrayData.displayName : ObjectNames.NicifyVariableName(fieldName);

            list.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    XMonoNode.NodePort port = node.GetPort(fieldName + " " + index);

                    if (hasArrayData && arrayData.propertyType != SerializedPropertyType.String)
                    {
                        if (arrayData.arraySize <= index)
                        {
                            GUI.Label(rect, "Array[" + index + "] data out of range");
                            return;
                        }
                        Rect labelRect = rect;
                        labelRect.width = 30;
                        Rect fieldRect = rect;
                        fieldRect.width -= labelRect.width;
                        fieldRect.x += labelRect.width;
                        SerializedProperty itemData = arrayData.GetArrayElementAtIndex(index);
                        if (NodeEditorPreferences.GetSettings().showPortButton(buttonAttribute))
                        {
                            NodeEditorUtilities.AddPortButtonPressed(port, GUI.Button(labelRect, $"{index}"));
                            EditorGUI.PropertyField(fieldRect, itemData, GUIContent.none, true);
                        }
                        else
                        {
                            switch (showBacking)
                            {
                                case XMonoNode.ShowBackingValue.Unconnected:
                                    if (port.IsConnected)
                                    {
                                        GUI.Label(labelRect, $"{index}");
                                    }
                                    else
                                    {
                                        GUI.Label(labelRect, $"{index}");
                                        EditorGUI.PropertyField(fieldRect, itemData, GUIContent.none, true);
                                    }
                                    break;
                                case XMonoNode.ShowBackingValue.Never:
                                    GUI.Label(labelRect, $"{index}");
                                    break;
                                case XMonoNode.ShowBackingValue.Always:
                                    GUI.Label(labelRect, $"{index}");
                                    EditorGUI.PropertyField(fieldRect, itemData, GUIContent.none, true);
                                    break;
                            }
                        }
                    }
                    else
                        GUI.Label(rect, port != null ? port.fieldName : "");
                    if (port != null)
                    {
                        Vector2 pos = rect.position + (port.IsOutput ? new Vector2(rect.width + 6, 0) : new Vector2(-36, 0));
                        NodeEditorGUILayout.PortField(pos, port);
                    }
                };
            list.elementHeightCallback =
                (int index) => {
                    if (hasArrayData) {
                        if (arrayData.arraySize <= index) return EditorGUIUtility.singleLineHeight;
                        SerializedProperty itemData = arrayData.GetArrayElementAtIndex(index);
                        return EditorGUI.GetPropertyHeight(itemData);
                    } else return EditorGUIUtility.singleLineHeight;
                };
            list.drawHeaderCallback =
                (Rect rect) => {
                    GUI.Label(rect, label);
                };
            list.onSelectCallback =
                (ReorderableList rl) => {
                    reorderableListIndex = rl.index;
                };
            list.onReorderCallback =
                (ReorderableList rl) => {
                    bool hasRect = false;
                    bool hasNewRect = false;
                    Rect rect = Rect.zero;
                    Rect newRect = Rect.zero;
                    // Move up
                    if (rl.index > reorderableListIndex) {
                        for (int i = reorderableListIndex; i < rl.index; ++i) {
                            XMonoNode.NodePort port = node.GetPort(fieldName + " " + i);
                            XMonoNode.NodePort nextPort = node.GetPort(fieldName + " " + (i + 1));
                            port.SwapConnections(nextPort);

                            // Swap cached positions to mitigate twitching
                            hasRect = NodeEditorWindow.current.portConnectionPoints.TryGetValue(port, out rect);
                            hasNewRect = NodeEditorWindow.current.portConnectionPoints.TryGetValue(nextPort, out newRect);
                            NodeEditorWindow.current.portConnectionPoints[port] = hasNewRect ? newRect : rect;
                            NodeEditorWindow.current.portConnectionPoints[nextPort] = hasRect ? rect : newRect;
                        }
                    }
                    // Move down
                    else {
                        for (int i = reorderableListIndex; i > rl.index; --i) {
                            XMonoNode.NodePort port = node.GetPort(fieldName + " " + i);
                            XMonoNode.NodePort nextPort = node.GetPort(fieldName + " " + (i - 1));
                            port.SwapConnections(nextPort);

                            // Swap cached positions to mitigate twitching
                            hasRect = NodeEditorWindow.current.portConnectionPoints.TryGetValue(port, out rect);
                            hasNewRect = NodeEditorWindow.current.portConnectionPoints.TryGetValue(nextPort, out newRect);
                            NodeEditorWindow.current.portConnectionPoints[port] = hasNewRect ? newRect : rect;
                            NodeEditorWindow.current.portConnectionPoints[nextPort] = hasRect ? rect : newRect;
                        }
                    }
                    // Apply changes
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();

                    // Move array data if there is any
                    if (hasArrayData) {
                        arrayData.MoveArrayElement(reorderableListIndex, rl.index);
                    }

                    // Apply changes
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    NodeEditorWindow.current.Repaint();
                    EditorApplication.delayCall += NodeEditorWindow.current.Repaint;
                };
            list.onAddCallback =
                (ReorderableList rl) => {
                    // Add dynamic port postfixed with an index number
                    string newName = fieldName + " 0";
                    int i = 0;
                    while (node.HasPort(newName)) newName = fieldName + " " + (++i);

                    if (io == XMonoNode.NodePort.IO.Output) node.AddDynamicOutput(type, connectionType, XMonoNode.TypeConstraint.None, newName);
                    else node.AddDynamicInput(type, connectionType, typeConstraint, newName);
                    serializedObject.Update();
                    EditorUtility.SetDirty(node as UnityEngine.Object);
                    if (hasArrayData) {
                        arrayData.InsertArrayElementAtIndex(arrayData.arraySize);
                    }
                    serializedObject.ApplyModifiedProperties();
                };
            list.onRemoveCallback =
                (ReorderableList rl) => {

                    var indexedPorts = node.DynamicPorts.Select(x => {
                        string[] split = x.fieldName.Split(' ');
                        if (split != null && split.Length == 2 && split[0] == fieldName) {
                            int i = -1;
                            if (int.TryParse(split[1], out i)) {
                                return new { index = i, port = x };
                            }
                        }
                        return new { index = -1, port = (XMonoNode.NodePort)null };
                    }).Where(x => x.port != null);
                    dynamicPorts = indexedPorts.OrderBy(x => x.index).Select(x => x.port).ToList();

                    int index = rl.index;

                    if (dynamicPorts[index] == null) {
                        Debug.LogWarning("No port found at index " + index + " - Skipped");
                    } else if (dynamicPorts.Count <= index) {
                        Debug.LogWarning("DynamicPorts[" + index + "] out of range. Length was " + dynamicPorts.Count + " - Skipped");
                    } else {

                        // Clear the removed ports connections
                        dynamicPorts[index].ClearConnections();
                        // Move following connections one step up to replace the missing connection
                        for (int k = index + 1; k < dynamicPorts.Count(); k++) {
                            for (int j = 0; j < dynamicPorts[k].ConnectionCount; j++) {
                                XMonoNode.NodePort other = dynamicPorts[k].GetConnection(j);
                                dynamicPorts[k].Disconnect(other);
                                dynamicPorts[k - 1].Connect(other);
                            }
                        }
                        // Remove the last dynamic port, to avoid messing up the indexing
                        node.RemoveDynamicPort(dynamicPorts[dynamicPorts.Count() - 1].fieldName);
                        serializedObject.Update();
                        EditorUtility.SetDirty(node as UnityEngine.Object);
                    }

                    if (hasArrayData && arrayData.propertyType != SerializedPropertyType.String) {
                        if (arrayData.arraySize <= index) {
                            Debug.LogWarning("Attempted to remove array index " + index + " where only " + arrayData.arraySize + " exist - Skipped");
                            Debug.Log(rl.list[0]);
                            return;
                        }
                        arrayData.DeleteArrayElementAtIndex(index);
                        // Error handling. If the following happens too often, file a bug report at https://github.com/Siccity/xNode/issues
                        if (dynamicPorts.Count <= arrayData.arraySize) {
                            while (dynamicPorts.Count <= arrayData.arraySize) {
                                arrayData.DeleteArrayElementAtIndex(arrayData.arraySize - 1);
                            }
                            UnityEngine.Debug.LogWarning("Array size exceeded dynamic ports size. Excess items removed.");
                        }
                        serializedObject.ApplyModifiedProperties();
                        serializedObject.Update();
                    }
                };

            if (hasArrayData) {
                int dynamicPortCount = dynamicPorts.Count;
                while (dynamicPortCount < arrayData.arraySize) {
                    // Add dynamic port postfixed with an index number
                    string newName = arrayData.name + " 0";
                    int i = 0;
                    while (node.HasPort(newName)) newName = arrayData.name + " " + (++i);
                    if (io == XMonoNode.NodePort.IO.Output) node.AddDynamicOutput(type, connectionType, typeConstraint, newName);
                    else node.AddDynamicInput(type, connectionType, typeConstraint, newName);
                    EditorUtility.SetDirty(node as UnityEngine.Object);
                    dynamicPortCount++;
                }
                while (arrayData.arraySize < dynamicPortCount) {
                    arrayData.InsertArrayElementAtIndex(arrayData.arraySize);
                }
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
            if (onCreation != null) onCreation(list);
            return list;
        }
    }
}