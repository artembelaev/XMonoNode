using System;
using UnityEngine;

namespace XMonoNode
{
    public enum NodeInspectorButtonShow
    {
        Settings = 0,
        Always = 1,
        Never = 2,
    }

    /// <summary> Makes a serializable field hidden in node in node graph, but shown in ordinary unity inspector </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class NodeInspectorButtonAttribute : Attribute
    {
        public NodeInspectorButtonShow ShowButton => showButton;

        private NodeInspectorButtonShow showButton = NodeInspectorButtonShow.Settings;

        public NodeInspectorButtonAttribute(NodeInspectorButtonShow showButton = NodeInspectorButtonShow.Settings)
        {
            this.showButton = showButton;
        }

    }

}