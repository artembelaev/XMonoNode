using System;

namespace XMonoNode
{
    /// <summary> Mark a serializable field inline with next field </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class InlineAttribute : Attribute
    {
        public bool connectedOnly = false;

        /// <param name="connectedOnly">Make port as inline only if it connected</param>
        public InlineAttribute(bool connectedOnly = false)
        {
            this.connectedOnly = connectedOnly;
        }
    }
}
