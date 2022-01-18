using System;

namespace XMonoNode
{
    /// <summary> Mark a serializable field as an input port. You can access this through <see cref="GetInputPort(string)"/> </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class InputAttribute : Attribute
    {
        public ShowBackingValue backingValue;
        public ConnectionType connectionType;
        [Obsolete("Use dynamicPortList instead")]
        public bool instancePortList
        {
            get
            {
                return dynamicPortList;
            }
            set
            {
                dynamicPortList = value;
            }
        }
        public bool dynamicPortList;
        public TypeConstraint typeConstraint;
        public string label;

        /// <summary> Mark a serializable field as an input port. You can access this through <see cref="GetInputPort(string)"/> </summary>
        /// <param name="backingValue">Should we display the backing value for this port as an editor field? </param>
        /// <param name="connectionType">Should we allow multiple connections? </param>
        /// <param name="typeConstraint">Constrains which input connections can be made to this port </param>
        /// <param name="dynamicPortList">If true, will display a reorderable list of inputs instead of a single port. Will automatically add and display values for lists and arrays </param>
        /// <param name="label">Caption of port in editor</param>
        public InputAttribute(ShowBackingValue backingValue = ShowBackingValue.Unconnected, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, bool dynamicPortList = false, string label = "")
        {
            this.backingValue = backingValue;
            this.connectionType = connectionType;
            this.dynamicPortList = dynamicPortList;
            this.typeConstraint = typeConstraint;
            this.label = label;
        }
    }
}
