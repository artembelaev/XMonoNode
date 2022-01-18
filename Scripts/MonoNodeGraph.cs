using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace XMonoNode
{
    /// <summary> Base class for all node graphs </summary>
    [Serializable]
    public class MonoNodeGraph : MonoBehaviour, INodeGraph, ISerializationCallbackReceiver
    {
        [SerializeField]
        private AnimatorUpdateMode updateMode = AnimatorUpdateMode.Normal;

        public AnimatorUpdateMode UpdateMode => updateMode;

        public float DeltaTime
        {
            get
            {
                switch (updateMode)
                {
                    case AnimatorUpdateMode.Normal:         return Time.deltaTime;
                    case AnimatorUpdateMode.AnimatePhysics: return Time.fixedDeltaTime;
                    // case AnimatorUpdateMode.UnscaledTime: return Time.unscaledDeltaTime;
                }
                return Time.unscaledDeltaTime;
            }
        }

        /// <summary> All nodes in the graph. <para/>
        /// See: <see cref="AddNode{T}"/> </summary>
        [SerializeField, HideInInspector] public MonoNode[] nodes = new MonoNode[0];

        public int NodesCount => nodes.Length;

        public INode[] GetNodes()
        {
            var result = new INode[nodes.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                result[i] = nodes[i];
            }
            return result;
        }

        /// <summary> Add a node to the graph by type </summary>
        public T AddNode<T>() where T : class, INode
        {
            return AddNode(typeof(T)) as T;
        }

        /// <summary> Placing it last in the nodes list </summary>
        public void MoveNodeToTop(INode node)
        {
            var castedNode = node as MonoNode;
            int index;
            while ((index = Array.IndexOf(nodes, castedNode)) != NodesCount - 1)
            {
                nodes[index] = nodes[index + 1];
                nodes[index + 1] = castedNode;
            }
        }

        /// <summary> Add a node to the graph by type </summary>
        public virtual INode AddNode(Type type)
        {
            MonoNode.graphHotfix = this;
            MonoNode node = gameObject.AddComponent(type) as MonoNode;
            node.graph = this;
            node.OnNodeEnable();
            var nodesList = new List<MonoNode>(nodes);
            nodesList.Add(node);
            nodes = nodesList.ToArray();
            return node;
        }

        //public static T DeepCopy<T>(T obj)
        //{
        //    if (obj == null)
        //        throw new ArgumentNullException("Object cannot be null");
        //    return (T)Process(obj, 0);
        //}

        //private static string levelIndent(int level)
        //{
        //    string levelIndent = "";
        //    for (int i = 0; i < level; ++i)
        //    {
        //        levelIndent += "    ";
        //    }
        //    return levelIndent;
        //}

        //static object Process(object obj, int level = 1)
        //{
        //    if (obj == null)
        //        return null;

        //    Type type=obj.GetType();

        //    // FIXME
        //    string indent = levelIndent(level);

        //    if (type.IsValueType || type == typeof(string))
        //    {
        //        Debug.LogFormat("  {0}{1} : {2} - VALUE", indent, obj.ToString(), type.Name);
        //        return obj;
        //    }
        //    else if (type.IsArray)
        //    {
        //        Debug.LogFormat("  {0}{1} : {2} - ARRAY", indent, obj.ToString(), type.Name);
        //        Type elementType=Type.GetType( type.FullName.Replace("[]",string.Empty));

        //        if (elementType == null || elementType == typeof(NodePort) || elementType.GetCustomAttribute(typeof(System.SerializableAttribute)) == null)
        //        {
        //            return null;
        //        }

        //        var array=obj as Array;
        //        Array copied= null;

        //        try
        //        {
        //            copied = Array.CreateInstance(elementType, array.Length);
        //        }
        //        catch
        //        {
        //            Debug.Log("catch " + type.Name);
        //            throw new ArgumentException("Activator.CreateInstance(elementType)");
        //        }

        //        for (int i = 0; i < array.Length; i++)
        //        {
        //            object value = array.GetValue(i);
        //            object valueCopy = Process(value, level + 1);
        //            copied.SetValue(valueCopy, i);
        //        }
        //        return Convert.ChangeType(copied, obj.GetType());
        //    }
        //    else if (type.IsClass)
        //    {
        //        Debug.LogFormat("  {0}{1} : {2} - CLASS", indent, obj.ToString(), type.Name);
        //        //object toret = null;
        //        //try
        //        //{
        //        //    toret = Activator.CreateInstance(obj.GetType());
        //        //}
        //        //catch
        //        //{
        //        //    Debug.Log("catch " + type.Name);
        //        //    throw new ArgumentException("Activator.CreateInstance(obj.GetType())");
        //        //}
        //        FieldInfo[] fields=type.GetFields(BindingFlags.Public|
        //                    BindingFlags.NonPublic|BindingFlags.Instance);
        //        //foreach (FieldInfo field in fields)
        //        //{
        //        //    Debug.LogFormat("  {0}{1} {2} {3}", indent, indent, field.FieldType.Name, field.Name);
        //        //}

        //        foreach (FieldInfo field in fields)
        //        {
        //            object fieldValue=field.GetValue(obj);
        //            if (fieldValue == null || field.Name == "ports" || field.Name == "graph" ||
        //                field.FieldType == typeof(NodePort) || field.FieldType == typeof(List<NodePort>))
        //                continue;
        //            object newValue = Process(fieldValue, level + 1);
        //            //if (newValue != null)
        //            //{
        //            //    field.SetValue(toret, newValue);
        //            //}
        //        }

        //        return null;//toret;
        //    }
        //    else
        //        throw new ArgumentException("Unknown type");
        //}


        ///// <summary> Creates a copy of the original node in the graph </summary>
        //public virtual INode CopyNode(INode original)
        //{
        //    MonoNode originalNode = original as MonoNode;
        //    if (originalNode == null)
        //    {
        //        throw new ArgumentException("MonoNodeGraph can only copy MonoNodes");
        //    }

        //    MonoNode.graphHotfix = this;
        //    MonoNode node = gameObject.AddComponent(original.GetType()) as MonoNode;

        //    // Copy values
        //    FieldInfo[] fields =  node.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //    foreach (FieldInfo field in fields)
        //    {

        //        object fieldValue=field.GetValue(originalNode);
        //        if (fieldValue == null || !field.FieldType.IsSerializable || field.Name == "ports" || field.Name == "graph" || field.FieldType == typeof(NodePort))
        //            continue;
        //        object newValue = Process(fieldValue);
        //        if (newValue != null)
        //        {
        //            field.SetValue(node, newValue);
        //        }
        //    }

        //    node.graph = this;
        //    node.ClearConnections();
        //    var nodesList = new List<MonoNode>(nodes);
        //    nodesList.Add(node);
        //    nodes = nodesList.ToArray();
        //    return node;
        //}

        /// <summary> Creates a copy of the original node in the graph </summary>
        public virtual INode CopyNode(INode original)
        {
            MonoNode originalNode = original as MonoNode;
            if (originalNode == null)
            {
                throw new ArgumentException("MonoNodeGraph can only copy MonoNodes");
            }

            MonoNode.graphHotfix = this;
            MonoNode node = gameObject.AddComponent(original.GetType()) as MonoNode;

            // Copy values
            FieldInfo[] fields =  node.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (FieldInfo field in fields)
            {
                if (field.Name != "ports")
                {
                    field.SetValue(node, field.GetValue(originalNode));
                }
            }


            node.graph = this;

            node.OnNodeEnable();

            node.ClearConnections();
            var nodesList = new List<MonoNode>(nodes);
            nodesList.Add(node);
            nodes = nodesList.ToArray();
            return node;
        }

        /// <summary> Safely remove a node and all its connections </summary>
        /// <param name="node"> The node to remove </param>
        public void RemoveNode(INode node)
        {
            node.ClearConnections();
            var nodesList = new List<MonoNode>(nodes);
            nodesList.Remove(node as MonoNode);
            nodes = nodesList.ToArray();
            if (Application.isPlaying) DestroyImmediate(node as UnityEngine.Object);
        }

        /// <summary> Remove all nodes and connections from the graph </summary>
        public void Clear()
        {
            //if (Application.isPlaying)
            //{
            //    foreach (MonoNode node in nodes)
            //    {
            //        if (nodes != null)
            //        {
            //            Destroy(node);
            //        }
            //    }
            //}
            nodes = new MonoNode[0];
        }

        /// <summary> Create a new deep copy of this graph </summary>
        public XMonoNode.INodeGraph Copy()
        {
            // Instantiate a new nodegraph instance
            MonoNodeGraph graph = Instantiate(this);
            return graph;
        }

        private void OnDestroy()
        {
            // Remove all nodes prior to graph destruction
            Clear();
        }

        protected virtual void Init()
        {

        }

        public void OnBeforeSerialize()
        {
            try // GetComponents() causes NullreferenceException in reset()
            {
                nodes = GetComponents<MonoNode>();
                Init();
            }
            catch
            {
            }
            MonoNode.graphHotfix = this;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].OnNodeEnable();
                    nodes[i].graph = this;
                }
            }
            MonoNode.graphHotfix = null;
        }

        public void OnAfterDeserialize()
        {
            
        }


        public System.Type getNodeType()
        {
            return typeof(MonoNode);
        }
    }
}
