using System;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    /// <summary> Used by <see cref="InputAttribute"/> and <see cref="OutputAttribute"/> to determine when to display the field value associated with a <see cref="NodePort"/> </summary>
    public enum ShowBackingValue
    {
        /// <summary> Never show the backing value </summary>
        Never,
        /// <summary> Show the backing value only when the port does not have any active connections </summary>
        Unconnected,
        /// <summary> Always show the backing value </summary>
        Always
    }

    public enum ConnectionType
    {
        /// <summary> Allow multiple connections</summary>
        Multiple,
        /// <summary> always override the current connection </summary>
        Override,
    }

    /// <summary> Tells which types of input to allow </summary>
    public enum TypeConstraint
    {
        /// <summary> Allow all types of input</summary>
        None,
        /// <summary> Allow connections where input value type is assignable from output value type (eg. ScriptableObject --> Object)</summary>
        Inherited,
        /// <summary> Allow only similar types </summary>
        Strict,
        /// <summary> Allow connections where output value type is assignable from input value type (eg. Object --> ScriptableObject)</summary>
        InheritedInverse,
        /// <summary> Allow connections where output value type is assignable from input value or input value type is assignable from output value type</summary>
        InheritedAny
    }

    public interface INode
    {
        string Name
        {
            get; set;
        }

        INodeGraph Graph
        {
            get;
        }

        Vector2 Position
        {
            get;
            set;
        }

        public enum ShowAttribState
        {
            ShowBase = 0,
            ShowAll = 1,
            Minimize = 2,
        }

        ShowAttribState ShowState
        {
            get;
            set;
        }

        IEnumerable<NodePort> DynamicInputs
        {
            get;
        }
        IEnumerable<NodePort> DynamicOutputs
        {
            get;
        }
        IEnumerable<NodePort> DynamicPorts
        {
            get;
        }
        IEnumerable<NodePort> Inputs
        {
            get;
        }
        IEnumerable<NodePort> InstanceInputs
        {
            get;
        }
        IEnumerable<NodePort> InstanceOutputs
        {
            get;
        }
        IEnumerable<NodePort> InstancePorts
        {
            get;
        }
        IEnumerable<NodePort> Outputs
        {
            get;
        }
        IEnumerable<NodePort> Ports
        {
            get;
        }

        NodePort AddDynamicInput(Type type, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null);
        NodePort AddDynamicOutput(Type type, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null);
        NodePort AddInstanceInput(Type type, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null);
        NodePort AddInstanceOutput(Type type, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null);

        void ClearConnections();
        void ClearDynamicPorts();
        void ClearInstancePorts();
        NodePort GetInputPort(string fieldName);
        T GetInputValue<T>(string fieldName, T fallback = default);
        T[] GetInputValues<T>(string fieldName, params T[] fallback);
        NodePort GetOutputPort(string fieldName);
        NodePort GetPort(string fieldName);
        object GetValue(NodePort port);
        bool HasPort(string fieldName);
        void OnCreateConnection(NodePort from, NodePort to);
        void OnRemoveConnection(NodePort port);
        void RemoveDynamicPort(NodePort port);
        void RemoveDynamicPort(string fieldName);
        void RemoveInstancePort(NodePort port);
        void RemoveInstancePort(string fieldName);
        void UpdatePorts();
        void VerifyConnections();
    }
}