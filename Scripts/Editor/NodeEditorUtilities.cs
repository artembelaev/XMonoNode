using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using XMonoNode;
using Object = UnityEngine.Object;

namespace XMonoNodeEditor
{
    /// <summary> A set of editor-only utilities and extensions for xNode </summary>
    public static class NodeEditorUtilities
    {
        private static Dictionary<NodePort, bool> portButtonPressed = new Dictionary<NodePort, bool>();

        public static void AddPortButtonPressed(NodePort port, bool pressed)
        {
            portButtonPressed[port] = pressed;
        }

        public static bool GetPortButtonPressed(NodePort port)
        {
            portButtonPressed.TryGetValue(port, out bool pressed);
            return pressed;
        }

        public static void ClearPortButtonPressed()
        {
            portButtonPressed.Clear();
        }

        /// <summary>C#'s Script Icon [The one MonoBhevaiour Scripts have].</summary>
        private static Texture2D scriptIcon = (EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);

        /// Saves Attribute from Type+Field for faster lookup. Resets on recompiles.
        private static Dictionary<Type, Dictionary<string, Dictionary<Type, Attribute>>> typeAttributes = new Dictionary<Type, Dictionary<string, Dictionary<Type, Attribute>>>();

        /// Saves ordered PropertyAttribute from Type+Field for faster lookup. Resets on recompiles.
        private static Dictionary<Type, Dictionary<string, List<PropertyAttribute>>> typeOrderedPropertyAttributes = new Dictionary<Type, Dictionary<string, List<PropertyAttribute>>>();

        

        public static bool GetAttrib<T>(Type classType, out T attribOut) where T : Attribute {
            object[] attribs = classType.GetCustomAttributes(typeof(T), false);
            return GetAttrib(attribs, out attribOut);
        }

        public static bool GetAttrib<T>(object[] attribs, out T attribOut) where T : Attribute {
            for (int i = 0; i < attribs.Length; i++) {
                if (attribs[i] is T) {
                    attribOut = attribs[i] as T;
                    return true;
                }
            }
            attribOut = null;
            return false;
        }

        public static bool GetAttrib<T>(Type classType, string fieldName, out T attribOut) where T : Attribute {
            // If we can't find field in the first run, it's probably a private field in a base class.
            FieldInfo field = classType.GetFieldInfo(fieldName);
            // This shouldn't happen. Ever.
            if (field == null) {
                Debug.LogWarning("Field " + fieldName + " couldnt be found");
                attribOut = null;
                return false;
            }
            object[] attribs = field.GetCustomAttributes(typeof(T), true);
            return GetAttrib(attribs, out attribOut);
        }

        public static bool HasAttrib<T>(object[] attribs) where T : Attribute {
            for (int i = 0; i < attribs.Length; i++) {
                if (attribs[i].GetType() == typeof(T)) {
                    return true;
                }
            }
            return false;
        }

        public static bool GetCachedAttrib<T>(SerializedProperty property, out T attribOut) where T : Attribute
        {
            attribOut = null;
            if (property != null && property.serializedObject != null && property.serializedObject.targetObject != null)
            {
                INode node = property.serializedObject.targetObject as INode;

                if (node == null)
                {
                    return false;
                }

                return NodeEditorUtilities.GetCachedAttrib(node.GetType(), property.name, out attribOut);
            }
            else
            {

                return false;
            }
        }

        public static bool GetCachedAttrib<T>(NodePort port, out T attribOut) where T : Attribute
        {
            attribOut = null;
            if (port == null || port.node == null || string.IsNullOrEmpty(port.fieldName))
            {
                return false;
            }

            string[] fieldNameParts = port.fieldName.Split(' '); // if port is dynamic

            string fieldName = fieldNameParts[0];

            return GetCachedAttrib(port.node.GetType(), fieldName, out attribOut);
        }

        public static bool GetCachedAttrib<T>(Type classType, string fieldName, out T attribOut) where T : Attribute {
            Dictionary<string, Dictionary<Type, Attribute>> typeFields;
            if (!typeAttributes.TryGetValue(classType, out typeFields)) {
                typeFields = new Dictionary<string, Dictionary<Type, Attribute>>();
                typeAttributes.Add(classType, typeFields);
            }

            Dictionary<Type, Attribute> typeTypes;
            if (!typeFields.TryGetValue(fieldName, out typeTypes)) {
                typeTypes = new Dictionary<Type, Attribute>();
                typeFields.Add(fieldName, typeTypes);
            }

            Attribute attr;
            if (!typeTypes.TryGetValue(typeof(T), out attr)) {
                if (GetAttrib<T>(classType, fieldName, out attribOut)) {
                    typeTypes.Add(typeof(T), attribOut);
                    return true;
                } else typeTypes.Add(typeof(T), null);
            }

            if (attr == null) {
                attribOut = null;
                return false;
            }

            attribOut = attr as T;
            return true;
        }

        public static List<PropertyAttribute> GetCachedPropertyAttribs(Type classType, string fieldName) {
            Dictionary<string, List<PropertyAttribute>> typeFields;
            if (!typeOrderedPropertyAttributes.TryGetValue(classType, out typeFields)) {
                typeFields = new Dictionary<string, List<PropertyAttribute>>();
                typeOrderedPropertyAttributes.Add(classType, typeFields);
            }

            List<PropertyAttribute> typeAttributes;
            if (!typeFields.TryGetValue(fieldName, out typeAttributes)) {
                FieldInfo field = classType.GetFieldInfo(fieldName);
                object[] attribs = field.GetCustomAttributes(typeof(PropertyAttribute), true);
                typeAttributes = attribs.Cast<PropertyAttribute>().Reverse().ToList(); //Unity draws them in reverse
                typeFields.Add(fieldName, typeAttributes);
            }

            return typeAttributes;
        }

        public static bool IsMac() {
#if UNITY_2017_1_OR_NEWER
            return SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX;
#else
            return SystemInfo.operatingSystem.StartsWith("Mac");
#endif
        }

        /// <summary> Returns true if this can be casted to <see cref="Type"/></summary>
        public static bool IsCastableTo(this Type from, Type to) {
            if (to.IsAssignableFrom(from)) return true;
            var methods = from.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(
                    m => m.ReturnType == to &&
                    (m.Name == "op_Implicit" ||
                        m.Name == "op_Explicit")
                );
            return methods.Count() > 0;
        }

        /// <summary>
        /// Looking for ports with value Type compatible with a given type. 
        /// </summary>
        /// <param name="nodeType">Node to search</param>
        /// <param name="compatibleType">Type to find compatiblities</param>
        /// <param name="direction"></param>
        /// <returns>True if NodeType has some port with value type compatible</returns>
        public static bool HasCompatiblePortType(Type nodeType, Type compatibleType, XMonoNode.NodePort.IO direction = XMonoNode.NodePort.IO.Input) {
            Type findType = typeof(XMonoNode.InputAttribute);
            if (direction == XMonoNode.NodePort.IO.Output)
                findType = typeof(XMonoNode.OutputAttribute);

            //Get All fields from node type and we go filter only field with portAttribute.
            //This way is possible to know the values of the all ports and if have some with compatible value tue
            foreach (FieldInfo f in XMonoNode.NodeDataCache.GetNodeFields(nodeType)) {
                var portAttribute = f.GetCustomAttributes(findType, false).FirstOrDefault();
                if (portAttribute != null) {
                    if (IsCastableTo(f.FieldType, compatibleType)) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Filter only node types that contains some port value type compatible with an given type
        /// </summary>
        /// <param name="nodeTypes">List with all nodes type to filter</param>
        /// <param name="compatibleType">Compatible Type to Filter</param>
        /// <returns>Return Only Node Types with ports compatible, or an empty list</returns>
        public static List<Type> GetCompatibleNodesTypes(Type[] nodeTypes, Type compatibleType, XMonoNode.NodePort.IO direction = XMonoNode.NodePort.IO.Input) {
            //Result List
            List<Type> filteredTypes = new List<Type>();

            //Return empty list
            if (nodeTypes == null) { return filteredTypes; }
            if (compatibleType == null) { return filteredTypes; }

            //Find compatiblity
            foreach (Type findType in nodeTypes) {
                if (HasCompatiblePortType(findType, compatibleType, direction)) {
                    filteredTypes.Add(findType);
                }
            }

            return filteredTypes;
        }

        /// <summary> Returns the default name for the node type. </summary>
        public static string NodeDefaultName(Type type) {
            string typeName = type.Name;
            // Automatically remove redundant 'Node' postfix
            if (typeName.EndsWith("Node")) typeName = typeName.Substring(0, typeName.LastIndexOf("Node"));
            typeName = UnityEditor.ObjectNames.NicifyVariableName(typeName);
            return typeName;
        }

        /// <summary> Returns the default creation path for the node type. </summary>
        public static string NodeDefaultPath(Type type) {
            string typePath = type.ToString().Replace('.', '/');
            // Automatically remove redundant 'Node' postfix
            if (typePath.EndsWith("Node")) typePath = typePath.Substring(0, typePath.LastIndexOf("Node"));
            typePath = UnityEditor.ObjectNames.NicifyVariableName(typePath);
            return typePath;
        }

        /// <summary>Creates a new C# Class.</summary>
        [MenuItem("Assets/Create/xMonoNode/Node C# Script", false, 87)]
        private static void CreateNode() {
            string[] guids = AssetDatabase.FindAssets("xNode_NodeTemplate.cs");
            if (guids.Length == 0) {
                Debug.LogWarning("xNode_NodeTemplate.cs.txt not found in asset database");
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            CreateFromTemplate(
                "NewNode.cs",
                path
            );
        }

        /// <summary>Creates a new C# Class.</summary>
        [MenuItem("Assets/Create/xMonoNode/NodeGraph C# Script", false, 88)]
        private static void CreateGraph() {
            string[] guids = AssetDatabase.FindAssets("xNode_NodeGraphTemplate.cs");
            if (guids.Length == 0) {
                Debug.LogWarning("xNode_NodeGraphTemplate.cs.txt not found in asset database");
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            CreateFromTemplate(
                "NewNodeGraph.cs",
                path
            );
        }

        /// <summary>Creates a new C# Class.</summary>
        [MenuItem("Assets/Create/xMonoNode/MonoNode C# Script", false, 89)]
        private static void CreateMonoNode()
        {
            string[] guids = AssetDatabase.FindAssets("xMonoNode_MonoNodeTemplate.cs");
            if (guids.Length == 0)
            {
                Debug.LogWarning("xMonoNode_MonoNodeTemplate.cs.txt not found in asset database");
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            CreateFromTemplate(
                "NewMonoNode.cs",
                path
            );
        }

        /// <summary>Creates a new C# Class.</summary>
        [MenuItem("Assets/Create/xMonoNode/MonoNodeGraph C# Script", false, 90)]
        private static void CreateMonoNodeGraph()
        {
            string[] guids = AssetDatabase.FindAssets("xMonoNode_MonoNodeGraphTemplate.cs");
            if (guids.Length == 0)
            {
                Debug.LogWarning("xMonoNode_MonoNodeGraphTemplate.cs.txt not found in asset database");
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            CreateFromTemplate(
                "NewMonoNodeGraph.cs",
                path
            );
        }

        [MenuItem("Assets/Create/xMonoNode/FlowNode C# Script", false, 91)]
        private static void CreateFlowNode()
        {
            string[] guids = AssetDatabase.FindAssets("xMonoNode_FlowNodeTemplate.cs");
            if (guids.Length == 0)
            {
                Debug.LogWarning("xMonoNode_FlowNodeTemplate.cs.txt not found in asset database");
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            CreateFromTemplate(
                "NewFlowNode.cs",
                path
            );
        }

        /// <summary>Creates a new C# Class.</summary>
        [MenuItem("Assets/Create/xMonoNode/FlowNodeGraph C# Script", false, 92)]
        private static void CreateFlowNodeGraph()
        {
            string[] guids = AssetDatabase.FindAssets("xMonoNode_FlowNodeGraphTemplate.cs");
            if (guids.Length == 0)
            {
                Debug.LogWarning("xMonoNode_FlowNodeGraphTemplate.cs.txt not found in asset database");
                return;
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            CreateFromTemplate(
                "NewFlowNodeGraph.cs",
                path
            );
        }

        public static void CreateFromTemplate(string initialName, string templatePath) {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                ScriptableObject.CreateInstance<DoCreateCodeFile>(),
                initialName,
                scriptIcon,
                templatePath
            );
        }

        /// Inherits from EndNameAction, must override EndNameAction.Action
        public class DoCreateCodeFile : UnityEditor.ProjectWindowCallback.EndNameEditAction {
            public override void Action(int instanceId, string pathName, string resourceFile) {
                Object o = CreateScript(pathName, resourceFile);
                ProjectWindowUtil.ShowCreatedAsset(o);
            }
        }

        /// <summary>Creates Script from Template's path.</summary>
        internal static UnityEngine.Object CreateScript(string pathName, string templatePath) {
            string className = Path.GetFileNameWithoutExtension(pathName).Replace(" ", string.Empty);
            string templateText = string.Empty;

            UTF8Encoding encoding = new UTF8Encoding(true, false);

            if (File.Exists(templatePath)) {
                /// Read procedures.
                StreamReader reader = new StreamReader(templatePath);
                templateText = reader.ReadToEnd();
                reader.Close();

                templateText = templateText.Replace("#SCRIPTNAME#", className);
                templateText = templateText.Replace("#NOTRIM#", string.Empty);
                /// You can replace as many tags you make on your templates, just repeat Replace function
                /// e.g.:
                /// templateText = templateText.Replace("#NEWTAG#", "MyText");

                /// Write procedures.

                StreamWriter writer = new StreamWriter(Path.GetFullPath(pathName), false, encoding);
                writer.Write(templateText);
                writer.Close();

                AssetDatabase.ImportAsset(pathName);
                return AssetDatabase.LoadAssetAtPath(pathName, typeof(Object));
            } else {
                Debug.LogError(string.Format("The template file was not found: {0}", templatePath));
                return null;
            }
        }
    }
}
